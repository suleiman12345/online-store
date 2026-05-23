// <copyright file="Program.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

using FluentValidation;
using FluentValidation.AspNetCore;
using OnlineStore.Infrastructure;
using OnlineStore.Infrastructure.Data;
using OnlineStore.Application;
using OnlineStore.Application.Validators;

var builder = WebApplication.CreateBuilder(args);

/// <summary>
/// Controllers + Swagger
/// </summary>
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<ProductCreateValidator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => policy
        .WithOrigins(
            "http://localhost:5276",
            "https://localhost:7128")
        .AllowAnyHeader()
        .AllowAnyMethod());
});

/// <summary>
/// Infrastructure (DbContext + Application + Repositories)
/// </summary>
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

/// <summary>
/// Dev pipeline
/// </summary>
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseAuthorization();

app.MapControllers();

/// <summary>
/// DB migration + seeding
/// </summary>
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DbInitializer.InitializeAsync(context);
}

app.Run();
