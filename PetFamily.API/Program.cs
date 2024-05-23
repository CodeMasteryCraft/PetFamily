using PetFamily.API.Middlewares;
using PetFamily.API.Validation;
using PetFamily.Application;
using PetFamily.Infrastructure;
using PetFamily.Infrastructure.BackgroundServices;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddHostedService<ImageCleanupService>();
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddFluentValidationAutoValidation(configuration =>
{
    configuration.OverrideDefaultResultFactoryWith<CustomResultFactory>();
});

builder.Services.AddHttpLogging(options => { });


// builder.Services.AddHangfire(configuration => configuration
//     .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
//     .UseSimpleAssemblyNameTypeSerializer()
//     .UseRecommendedSerializerSettings()
//     .UsePostgreSqlStorage(c => c
//         .UseNpgsqlConnection(builder.Configuration.GetConnectionString("PetFamily"))));


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
app.UseHttpLogging();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

// app.UseHangfireDashboard();
// app.MapHangfireDashboard("/dashboard");

app.Run();