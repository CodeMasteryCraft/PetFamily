using CSharpFunctionalExtensions;
using Hangfire;
using Hangfire.PostgreSql;
using PetFamily.API.Middlewares;
using PetFamily.API.Validation;
using PetFamily.Application;
using PetFamily.Infrastructure;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddFluentValidationAutoValidation(configuration =>
{
    configuration.OverrideDefaultResultFactoryWith<CustomResultFactory>();
});

builder.Services.AddHttpLogging(options => { });

// add hangfire client
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UsePostgreSqlStorage(c => c
        .UseNpgsqlConnection(builder.Configuration.GetConnectionString("HangFire"))));

// add hangfire server
builder.Services.AddHangfireServer();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpLogging();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.UseHangfireDashboard();
app.MapHangfireDashboard("/dashboard");

app.Run();

