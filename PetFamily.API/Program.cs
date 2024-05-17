using System.Text;
using System.Text.Encodings.Web;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetFamily.API.Middlewares;
using PetFamily.API.Validation;
using PetFamily.Application;
using PetFamily.Domain.Entities;
using PetFamily.Infrastructure;
using PetFamily.Infrastructure.DbContexts;
using PetFamily.Infrastructure.Repositories;
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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = "key18key18key18key18key18key18key18key18key18key18";

        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        options.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = symmetricKey
        };
    });

builder.Services.AddAuthorization();

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

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<PetFamilyWriteDbContext>();
    await dbContext.Database.MigrateAsync();

    // var passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("admin");
    //
    // var admin = new User("admin", passwordHash, Role.Admin);
    //
    // await dbContext.Users.AddAsync(admin);
    // await dbContext.SaveChangesAsync();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpLogging();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// app.UseHangfireDashboard();
// app.MapHangfireDashboard("/dashboard");

app.Run();