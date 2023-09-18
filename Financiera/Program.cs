using Financiera.DBContext;
using Financiera.DBContext.Repository;
using Financiera.Domain;
using Financiera.Models;
using Financiera.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddDbContext<FinancieraContext>(options 
    => options.UseSqlServer(builder.Configuration.GetConnectionString("MyContextConnection")));

//Repository
builder.Services.AddTransient<IRepository<Cliente>, Repository<Cliente>>();
builder.Services.AddTransient<IRepository<Genero>, Repository<Genero>>();
builder.Services.AddTransient<IRepository<Cuenta>, Repository<Cuenta>>();
builder.Services.AddTransient<IRepository<TipoCuenta>, Repository<TipoCuenta>>();
builder.Services.AddTransient<IRepository<Movimiento>, Repository<Movimiento>>();

//AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

//Register Services
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<CuentaService>();
builder.Services.AddScoped<MovimientoService>();


//Register Domain
builder.Services.AddScoped<ClienteDomain>();
builder.Services.AddScoped<CuentaDomain>();
builder.Services.AddScoped<MovimientoDomain>();

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



