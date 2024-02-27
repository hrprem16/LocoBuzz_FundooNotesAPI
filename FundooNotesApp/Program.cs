﻿using Repository_Layer.Context;
using Microsoft.EntityFrameworkCore;
using Repository_Layer.Interfaces;
using Manager_Layer.Interfaces;
using Manager_Layer.Services;
using Repository_Layer.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<FundoContext>(x => x.UseSqlServer(builder.Configuration["ConnectionStrings:dbconnection"]));
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserManager, UserManager>();
builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<FundoContext>(x => x.UseSqlServer(builder.Configuration["ConnectionStrings:FundoAppdb"]));

builder.Services.AddMassTransit(x =>
{
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
    {
        config.UseHealthCheck(provider);
        config.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    }));
});
builder.Services.AddMassTransitHostedService();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c => {
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.IgnoreObsoleteActions();
    c.IgnoreObsoleteProperties();
    c.CustomSchemaIds(type => type.FullName);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



