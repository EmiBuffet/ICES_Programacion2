using Scalar.AspNetCore;
using FacturasAPI.Datos;
using FacturasAPI.Endpoints;
using FacturasAPI.Logica;
using FacturasAPI.Entidades;
using Microsoft.EntityFrameworkCore;
using FacturasAPI.Repositorios;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<FacturasDBContext>(opt => 
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IFacturasRepositorio, FacturasRepositorio>();
builder.Services.AddScoped<IFacturasLogica, FacturasLogica>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapFacturasEndpoints();

app.Run();

