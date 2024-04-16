using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Common;
using PetFamily.Infrastructure;
using PetFamily.Infrastructure.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddApplication();
builder.Services.AddInfastructure();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();