using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Immutable;
using EcoMonitor.BLL.Services;
using EcoMonitor.BLL.Services.Abstractions;
using EcoMonitor.Core.Models;
using EcoMonitor.Core.Repository;
using System.Text.Unicode;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EcoContext>(options =>
    options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ECO;Trusted_Connection=True;TrustServerCertificate=True;"));
builder.Services.AddScoped<IRepository<Pollutions>, Repository<Pollutions>>();
builder.Services.AddScoped<IRepository<Factories>, Repository<Factories>>();
builder.Services.AddScoped<IRepository<Calculations>, Repository<Calculations>>();
builder.Services.AddTransient<ICalculationService, CalculationService>();
builder.Services.AddTransient<IFactoryService, FactoryService>();
builder.Services.AddTransient<IPollutionService, PollutionService>();
builder.Services.AddAutoMapper(typeof(ConfigurationMapper));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", builder =>
    {
        builder.WithOrigins("http://localhost:63342") // Укажите разрешённые источники
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // Если вы используете куки или аутентификацию
    });
});
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("uk-UA");
    options.SupportedCultures = new List<CultureInfo> { new CultureInfo("uk-UA") };
    options.SupportedUICultures = new List<CultureInfo> { new CultureInfo("uk-UA") };
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigins"); // Включение политики CORS

app.UseAuthorization();

app.MapControllers();

app.Run();
