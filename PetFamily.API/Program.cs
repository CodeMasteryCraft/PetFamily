using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetFamily.API.Authorization;
using PetFamily.API.Controllers;
using PetFamily.API.Extensions;
using PetFamily.API.Middlewares;
using PetFamily.API.Validation;
using PetFamily.Application;
using PetFamily.Infrastructure;
using PetFamily.Infrastructure.DbContexts;
using PetFamily.Infrastructure.Jobs;
using PetFamily.Infrastructure.Kafka;
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

builder.Services.AddSingleton<KafkaMessageProducer>();
builder.Services.AddHostedService<NotificationWorker>();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

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

app.MapPost("kafka",
    async (
        [FromQuery] string topic,
        [FromBody] string message,
        KafkaMessageProducer producer) =>
    {
        await producer.Publish(topic, message);
    });

app.UseHangfireDashboard();
app.MapHangfireDashboard();

HangfireWorker.StartRecurringJobs();

var kafkaConfig = new AdminClientConfig()
{
    BootstrapServers = "localhost:9092",
};

using var kafkaAdminClient = new AdminClientBuilder(kafkaConfig).Build();

var metaData = kafkaAdminClient.GetMetadata(TimeSpan.FromSeconds(5));

var topic = metaData.Topics.FirstOrDefault(t => t.Topic == "test-topic");

if (topic is null)
{
    var topicSpecification = new TopicSpecification()
    {
        Name = "test-topic",
        NumPartitions = 2
    };

    await kafkaAdminClient.CreateTopicsAsync([topicSpecification]);
}

app.Run();