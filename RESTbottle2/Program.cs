using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using RESTbottle2;
using RESTbottle2.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register repository that uses EF DbContext
builder.Services.AddScoped<IBottlesRepository, BottlesRepositoryDatabaseEF>();

// OpenAPI
builder.Services.AddOpenApi();

var app = builder.Build();

// Ensure the local database is created
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BottlesDbContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
