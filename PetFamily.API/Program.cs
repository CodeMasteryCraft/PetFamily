using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PetFamily.API.Authorization;
using PetFamily.API.Controllers;
using PetFamily.API.Extensions;
using PetFamily.API.Middlewares;
using PetFamily.API.Validation;
using PetFamily.Application;
using PetFamily.Application.Models;
using PetFamily.Infrastructure;
using PetFamily.Infrastructure.DbContexts;
using PetFamily.Infrastructure.Jobs;
using PetFamily.Infrastructure.Kafka;
using PetFamily.Infrastructure.TelegramBot;
using Serilog;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Debug()
    .WriteTo.Seq(builder.Configuration.GetSection("Seq").Value
                 ?? throw new ApplicationException("Seq configuration is empty"))
    .CreateLogger();

builder.Services.AddSwagger();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSerilog();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddInfrastructureKafka(builder.Configuration)
    .AddInfrastructureTelegram(builder.Configuration);

builder.Services.AddFluentValidationAutoValidation(configuration =>
{
    configuration.OverrideDefaultResultFactoryWith<CustomResultFactory>();
});

builder.Services.AddAuth(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy
                .WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

var app = builder.Build();

var scope = app.Services.CreateScope();

var dbContext = scope.ServiceProvider.GetRequiredService<PetFamilyWriteDbContext>();

await dbContext.Database.MigrateAsync();

app.UseMiddleware<ExceptionMiddleware>();
app.UseSerilogRequestLogging();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapPost("notification",
    async (
        [FromQuery] Guid userId,
        [FromBody] string message,
        [FromServices] IOptions<KafkaOptions> kafkaOptions,
        [FromServices] KafkaProducer<Notification> producer) =>
    {
        var notification = new Notification(userId, message);

        await producer.Publish(kafkaOptions.Value.NotificationsTopic, notification);
    });

app.UseHangfireDashboard();
app.MapHangfireDashboard();

HangfireWorker.StartRecurringJobs();

var kafkaOptions = app.Services.GetRequiredService<IOptions<KafkaOptions>>().Value;

var kafkaConfig = new AdminClientConfig()
{
    BootstrapServers = kafkaOptions.Host,
};

using var kafkaAdminClient = new AdminClientBuilder(kafkaConfig).Build();

var metaData = kafkaAdminClient.GetMetadata(TimeSpan.FromSeconds(5));

var topic = metaData.Topics.FirstOrDefault(t => t.Topic == kafkaOptions.NotificationsTopic);

if (topic is null)
{
    var topicSpecification = new TopicSpecification
    {
        Name = kafkaOptions.NotificationsTopic,
        NumPartitions = kafkaOptions.NotificationsTopicPartitions
    };

    await kafkaAdminClient.CreateTopicsAsync([topicSpecification]);
}

app.Run();