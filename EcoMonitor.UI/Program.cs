using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EcoMonitor.BLL.Services;
using EcoMonitor.BLL.Services.Abstractions;
using EcoMonitor.Core.Repository;
using AutoMapper;
using EcoMonitor.Core.Models;
using EcoMonitor.UI.Tax;
using EcoMonitor.UI.Tax.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Text;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddAutoMapper(typeof(ConfigurationMapper));
        services.AddScoped<ICalculationService, CalculationService>();
        services.AddDbContext<EcoContext>(options =>
            options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ECO;Trusted_Connection=True;TrustServerCertificate=True;"));
        services.AddScoped<IRepository<Pollutions>, Repository<Pollutions>>();
        services.AddScoped<IRepository<Factories>, Repository<Factories>>();
        services.AddScoped<IRepository<Calculations>, Repository<Calculations>>();
        services.AddScoped<IRepository<RadioCreation>, Repository<RadioCreation>>();
        services.AddScoped<IRepository<TempStorage>, Repository<TempStorage>>();
        services.AddScoped<IRepository<Risk>, Repository<Risk>>();
        services.AddScoped<IFactoryService, FactoryService>();
        services.AddScoped<IPollutionService, PollutionService>();
        services.AddScoped<IRiskService, RiskService>();
        services.AddScoped<ITaxCalculator, TaxCalculator>();
    })
    .Build();

var taxCalculator = builder.Services.GetRequiredService<ITaxCalculator>();
await RunTaxCalculationMenu(taxCalculator);

static async Task RunTaxCalculationMenu(ITaxCalculator calculator)
{
    Console.OutputEncoding = Encoding.UTF8;
    Console.InputEncoding = Encoding.UTF8;
    while (true)
    {
        Console.WriteLine("\nОберіть тип податку для розрахунку:");
        Console.WriteLine("1. Податок за викиди в атмосферне повітря");
        Console.WriteLine("2. Податок за скиди у водні об'єкти");
        Console.WriteLine("3. Податок за розміщення відходів");
        Console.WriteLine("4. Податок за утворення радіоактивних відходів");
        Console.WriteLine("5. Податок за тимчасове зберігання радіоактивних відходів");
        Console.WriteLine("6. Розрахунок ризику для забруднення населення");

        Console.WriteLine("0. Вихід");

        var choice = Console.ReadLine();

        try
        {
            switch (choice)
            {
                case "1":
                    await calculator.CalculateEmissionTax();
                    break;
                case "2":
                    await calculator.CalculateWaterPollutionTax();
                    break;
                case "3":
                    await calculator.CalculateWasteTax();
                    break;
                case "4":
                    await calculator.CalculateRadioactiveWasteTax();
                    break;
                case "5":
                    await calculator.CalculateRadioactiveStorageTax();
                    break;
                case "6":
                    await calculator.CalculateRiskAsync();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Некоректний вибір");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }
}