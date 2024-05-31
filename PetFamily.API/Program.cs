using Hangfire;
using Microsoft.AspNetCore.Authorization;
using PetFamily.API.Authorization;
using PetFamily.API.Extensions;
using PetFamily.API.Middlewares;
using PetFamily.API.Validation;
using PetFamily.Application;
using PetFamily.Infrastructure;
using PetFamily.Infrastructure.Jobs;
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
builder.Services.AddSerilog();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddFluentValidationAutoValidation(configuration =>
{
    configuration.OverrideDefaultResultFactoryWith<CustomResultFactory>();
});

builder.Services.AddAuth(builder.Configuration);

builder.Services.AddSingleton<IAuthorizationHandler, PermissionsAuthorizationsHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseSerilogRequestLogging();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard();
app.MapHangfireDashboard();

HangfireWorker.StartRecurringJobs();

app.Run();