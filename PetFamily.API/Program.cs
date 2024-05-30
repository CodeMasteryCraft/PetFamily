using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using PetFamily.API.Authorization;
using PetFamily.API.Extensions;
using PetFamily.API.Middlewares;
using PetFamily.API.Validation;
using PetFamily.Application;
using PetFamily.Infrastructure;
using Serilog;
using PetFamily.Infrastructure.BackgroundServices;
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
builder.Services.AddHostedService<ImageCleanupService>();

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

// add hangfire client
// builder.Services.AddHangfire(configuration => configuration
//     .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
//     .UseSimpleAssemblyNameTypeSerializer()
//     .UseRecommendedSerializerSettings()
//     .UsePostgreSqlStorage(c => c
//         .UseNpgsqlConnection(builder.Configuration.GetConnectionString("HangFire"))));

// add hangfire server
// builder.Services.AddHangfireServer();

var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
//     using var scope = app.Services.CreateScope();
//     var dbContext = scope.ServiceProvider.GetRequiredService<PetFamilyWriteDbContext>();
//     await dbContext.Database.MigrateAsync();
//
//
//     var admin = new User("admin", "admin", Role.Admin);
//
//     await dbContext.Users.AddAsync(admin);
//     await dbContext.SaveChangesAsync();
// }

app.UseMiddleware<ExceptionMiddleware>();
app.UseSerilogRequestLogging();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// app.UseHangfireDashboard();
// app.MapHangfireDashboard("/dashboard");

app.Run();

namespace PetFamily.API
{
    public class AuthOptions : AuthenticationSchemeOptions
    {
    }
}