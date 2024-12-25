using System.ComponentModel.Design;
using System.Net.Http.Headers;
using EcoMonitor.BLL.DTO;
using EcoMonitor.BLL.Services;
using EcoMonitor.BLL.Services.Abstractions;
using EcoMonitor.Core.Models;
using EcoMonitor.Core.Repository;
using EcoMonitor.UI.Tax.Abstraction;

namespace EcoMonitor.UI.Tax;

public class TaxCalculator : ITaxCalculator
{
    private readonly ICalculationService _calculationService;
    private readonly IFactoryService _factoryService;
    private readonly IPollutionService _pollutionService;
    private readonly IRiskService _riskService;
    private readonly IRepository<RadioCreation> _radioService;
    private readonly IRepository<TempStorage> _tempStorageServ;
    private readonly IRepository<Risk> _riskRepository;


    public TaxCalculator(
        ICalculationService calculationService,
        IFactoryService factoryService,
        IPollutionService pollutionService,
        IRiskService riskService,
        IRepository<RadioCreation> radioService,
        IRepository<Risk> riskRepository,
        IRepository<TempStorage> tempStorageServ)
    {
        _calculationService = calculationService;
        _factoryService = factoryService;
        _pollutionService = pollutionService;
        _radioService = radioService;
        _tempStorageServ = tempStorageServ;
        _riskService = riskService;
        _riskRepository = riskRepository;
    }

    public async Task CalculateEmissionTax()
    {
        Console.Clear();
        Console.WriteLine("Розрахунок податку за викиди в атмосферне повітря");

        // Отримання списку підприємств
        var factories = _factoryService.GetAllFactories().ToList();
        Console.WriteLine("\nДоступні підприємства:");
        Console.Clear();
        foreach (var factory in factories)
        {
            Console.WriteLine($"{factory.Id}. {factory.Name}");
        }

        Console.Write("\nОберіть ID підприємства: ");
        int factoryId = int.Parse(Console.ReadLine());

        Console.Write("\n Виберіть рік за який рахуеться податок : ");
        string date = Console.ReadLine();
        try
        {
            var calcs = _calculationService.GetAllCalculations().Where(e => e.Date == date
                                                                           && e.FactoryId == factoryId && e.isAir == true).
                ToList();
            var pollutions = _pollutionService.GetAllPollutions();
            Console.Clear();
            Console.WriteLine("\nДо якого розрахованого забдруднення вирахувати податок:");

            foreach (var calc in calcs)
            {
                Console.WriteLine($"{calcs.IndexOf(calc) + 1}. {pollutions.Where(e => e.Id == calc.PollutionId).FirstOrDefault().Name}");
            }
            var calcId = int.Parse(Console.ReadLine()) - 1;
            var selectedCalc = calcs[calcId];
            var pollutionTaxRate = pollutions.FirstOrDefault(e => e.Id == selectedCalc.PollutionId)?.TaxRateAw ?? 0;

            // Create a new instance with updated values instead of modifying the tracked entity
            var updatedCalc = new CalculationDTO
            {
                Id = selectedCalc.Id,
                Date = selectedCalc.Date,
                FactoryId = selectedCalc.FactoryId,
                PollutionId = selectedCalc.PollutionId,
                GeneralEmission = selectedCalc.GeneralEmission,
                isAir = selectedCalc.isAir,
                Tax = selectedCalc.GeneralEmission * pollutionTaxRate
            };
            await _calculationService.UpdateCalculation(updatedCalc);
            Console.Clear();
            Console.WriteLine($"\nПодаток розраховано: {updatedCalc.Tax}");

        }
        catch (Exception ex)
        {
            throw new ArgumentNullException("Неправильний ввід", ex);
        }
    }

    public async Task CalculateWaterPollutionTax()
    {
        Console.Clear();
        Console.WriteLine("Розрахунок податку за викиди у воду");

        // Отримання списку підприємств
        var factories = _factoryService.GetAllFactories().ToList();
        Console.WriteLine("\nДоступні підприємства:");
        Console.Clear();
        foreach (var factory in factories)
        {
            Console.WriteLine($"{factory.Id}. {factory.Name}");
        }

        Console.Write("\nОберіть ID підприємства: ");
        int factoryId = int.Parse(Console.ReadLine());

        Console.Write("\n Виберіть рік за який рахуеться податок : ");
        string date = Console.ReadLine();
        try
        {
            float kf = 1;
            var calcs = _calculationService.GetAllCalculations().Where(e => e.Date == date
                                                                           && e.FactoryId == factoryId && e.isAir != true).
                ToList();
            var pollutions = _pollutionService.GetAllPollutions();
            Console.Clear();
            Console.WriteLine("\nДо якого розрахованого забдруднення вирахувати податок:");

            foreach (var calc in calcs)
            {
                Console.WriteLine($"{calcs.IndexOf(calc) + 1}. {pollutions.Where(e => e.Id == calc.PollutionId).FirstOrDefault().Name}");
            }
            var calcId = int.Parse(Console.ReadLine()) - 1;
            var selectedCalc = calcs[calcId];
            var pollutionTaxRate = pollutions.FirstOrDefault(e => e.Id == selectedCalc.PollutionId)?.TaxRateAw ?? 0;

            Console.WriteLine("Забруднення скидаються в ставки і озера? Так/Ні");
            if (Console.ReadLine() == "Так")
            {
                kf = 1.5f;
            }
            // Create a new instance with updated values instead of modifying the tracked entity
            var updatedCalc = new CalculationDTO
            {
                Id = selectedCalc.Id,
                Date = selectedCalc.Date,
                FactoryId = selectedCalc.FactoryId,
                PollutionId = selectedCalc.PollutionId,
                GeneralEmission = selectedCalc.GeneralEmission,
                isAir = selectedCalc.isAir,
                Tax = selectedCalc.GeneralEmission * pollutionTaxRate * kf
            };
            await _calculationService.UpdateCalculation(updatedCalc);
            Console.Clear();
            Console.WriteLine($"\nПодаток розраховано: {updatedCalc.Tax}");

        }
        catch (Exception ex)
        {
            throw new ArgumentNullException("Неправильний ввід", ex);
        }
    }

    public static readonly Dictionary<string, float> HazardClasses = new()
    {
        { "I",  1546.22f },
        { "II", 56.32f },
        { "III", 14.12f },
        { "IV", 5.5f }
    };

    public async Task CalculateWasteTax()
    {
        Console.Clear();
        Console.WriteLine("Розрахунок розміщення відходів");
        // Отримання списку підприємств
        var factories = _factoryService.GetAllFactories().ToList();
        Console.WriteLine("\nДоступні підприємства:");
        Console.Clear();
        foreach (var factory in factories)
        {
            Console.WriteLine($"{factory.Id}. {factory.Name}");
        }

        Console.Write("\nОберіть ID підприємства: ");
        int factoryId = int.Parse(Console.ReadLine());

        Console.Write("\n Виберіть рік за який рахуеться податок : ");
        string date = Console.ReadLine();
        try
        {

            var calcs = _calculationService.GetAllCalculations().Where(e => e.Date == date
                                                                            && e.FactoryId == factoryId &&
                                                                            e.isAir != true).ToList();
            var pollutions = _pollutionService.GetAllPollutions();
            Console.Clear();
            Console.WriteLine("\nДо якого розрахованого забдруднення вирахувати податок:");

            foreach (var calc in calcs)
            {
                Console.WriteLine(
                    $"{calcs.IndexOf(calc) + 1}. {pollutions.Where(e => e.Id == calc.PollutionId).FirstOrDefault().Name}");
            }

            var calcId = int.Parse(Console.ReadLine()) - 1;
            var selectedCalc = calcs[calcId];
            var pollution = pollutions.Where(e => e.Id == selectedCalc.PollutionId).FirstOrDefault();
            float taxRateP = 0.54f;
            HazardClasses.TryGetValue(pollution.DangerRate, out taxRateP);
            int KT = 1;
            int KO = 1;

            Console.WriteLine("Відходи розміщені на відстані менш ніж 3км від НП Так/Ні");
            if (Console.ReadLine() == "Так")
            {
                KT = 3;
            }

            Console.Clear();
            Console.WriteLine(
                "Відходи розміщені на звалищах, які забезпечують повне виключення забруднення атмосферного повітря або води Так/Ні");
            if (Console.ReadLine() == "Ні")
            {
                KT = 3;
            }

            Console.Clear();
            // Create a new instance with updated values instead of modifying the tracked entity
            var updatedCalc = new CalculationDTO
            {
                Id = selectedCalc.Id,
                Date = selectedCalc.Date,
                FactoryId = selectedCalc.FactoryId,
                PollutionId = selectedCalc.PollutionId,
                GeneralEmission = selectedCalc.GeneralEmission,
                isAir = selectedCalc.isAir,
                Tax = selectedCalc.GeneralEmission * taxRateP * KT * KO
            };
            await _calculationService.UpdateCalculation(updatedCalc);

            Console.Clear();
            Console.WriteLine($"\nПодаток розраховано: {updatedCalc.Tax}");
        }
        catch (Exception ex)
        {
            throw new ArgumentNullException("Неправильний ввід", ex);
        }
    }

    public async Task CalculateRiskAsync()
    {
        Console.Clear();
        Console.WriteLine("Розрахунок розміщення відходів");
        // Отримання списку підприємств
        var factories = _factoryService.GetAllFactories().ToList();
        var pollutants = _pollutionService.GetAllPollutions().Where(e => e.isAirPollution).ToList();

        Console.WriteLine("\nДоступні підприємства:");
        Console.Clear();
        foreach (var factory in factories)
        {
            Console.WriteLine($"{factory.Id}. {factory.Name}");
        }

        Console.Write("\nОберіть ID підприємства: ");
        int factoryId = int.Parse(Console.ReadLine());

        Console.WriteLine("\nДоступні забруднення:");
        Console.Clear();
        foreach (var pollutant in pollutants)
        {
            Console.WriteLine($"{pollutant.Id}. {pollutant.Name}");
        }

        Console.Write("\nОберіть ID забруднення: ");
        int pollutantId = int.Parse(Console.ReadLine());
        try
        {
            Console.Write("Введіть концентрацію (С) (мг/м3): ");
            float conc = float.Parse(Console.ReadLine());
            Console.Write("Введіть рефернтну концентрацію (RfC) (мг/м3): ");
            float rfc = float.Parse(Console.ReadLine());
            Console.Write("Введіть фактор канцерогенного потенціалу сполуки (SF), (мг/(кт*д))^3: ");
            float sf = float.Parse(Console.ReadLine());
            Console.Write("Введіть період: ");
            float t = int.Parse(Console.ReadLine());
            float hq = conc / rfc;
            float ladd = conc * 20 * 365 * 70 / (70 * 70 * 365);
            float cr = ladd * sf;
            var newRisk = new RiskDTO()
            {
                Conc = conc,
                Cr =cr,
                Date = t,
                FactoryId = factoryId,
                Hq =hq,
                Ladd = ladd,
                PollutionId = pollutantId,
                Rfc = rfc,
                Sf = sf
            };

            await _riskService.AddRisk(newRisk);
        }
        catch (Exception ex)
        {
            throw new ArgumentNullException("Неправильний ввід", ex);
        }
    }

    public async Task CalculateRadioactiveWasteTax()
    {
        const float EL_CONST = 0.0133f;
        Console.Clear();
        Console.WriteLine("Розрахунок розміщення відходів");
        // Отримання списку підприємств
        var factories = _factoryService.GetAllFactories().ToList();
        Console.WriteLine("\nДоступні підприємства:");
        Console.Clear();
        foreach (var factory in factories)
        {
            Console.WriteLine($"{factory.Id}. {factory.Name}");
        }

        Console.Write("\nОберіть ID підприємства: ");
        int factoryId = int.Parse(Console.ReadLine());

        try
        {
            Console.Write("Введіть фактичний обсяг електричної енергії (кВт·год): ");
            float electricityVolume = EL_CONST * float.Parse(Console.ReadLine());

            Console.Write("Чи є радіоактивні відходи накопичені до 1 квітня 2009 року? (Так/Ні): ");
            bool hasRadioactiveWaste = Console.ReadLine() == "Так";

            Console.Write("Ви розраховуєте податок в період з 1 квітня 2011 року до 1 квітня 2019 року? (Так/Ні): ");
            bool calculateTaxInPeriod = Console.ReadLine() == "Так";

            Console.Write("Введіть собівартість зберігання 1 м³ низькоактивних і середньоактивних радіоактивних відходів за базовий податковий період (грн): ");
            float lowActiveCost = float.Parse(Console.ReadLine());

            Console.Write("Введіть собівартість зберігання 1 м³ високоактивних радіоактивних відходів за базовий податковий період (грн): ");
            float highActiveCost = float.Parse(Console.ReadLine());

            Console.Write("Введіть фактичний об'єм низькоактивних і середньоактивних радіоактивних відходів за базовий податковий період (м³): ");
            float lowActiveVolume = float.Parse(Console.ReadLine());

            Console.Write("Введіть фактичний об'єм високоактивних радіоактивних відходів за базовий податковий період (м³): ");
            float highActiveVolume = float.Parse(Console.ReadLine());

            if (hasRadioactiveWaste)
            {
                Console.Write("Введіть собівартість зберігання 1 м³ низькоактивних і середньоактивних радіоактивних відходів за базовий податковий період (грн): до 1 квітня 2009 року (м³):");
                float lowActiveCost2009 = float.Parse(Console.ReadLine());

                Console.Write("Введіть собівартість зберігання 1 м³ високоактивних радіоактивних відходів за базовий податковий період (грн) :до 1 квітня 2009 року (м³): ");
                float highActiveCost2 = float.Parse(Console.ReadLine());
                Console.Write("Введіть фактичний об'єм низькоактивних і середньоактивних радіоактивних відходів до 1 квітня 2009 року (м³): ");
                float lowActiveVolume2009 = float.Parse(Console.ReadLine());

                Console.Write("Введіть фактичний об'єм високоактивних радіоактивних відходів до 1 квітня 2009 року (м³): ");
                float highActiveVolume2009 = float.Parse(Console.ReadLine());
                var newRadioCreation = new RadioCreation()
                {
                    FactoryId = factoryId,
                    C1ns = lowActiveCost,
                    C2ns = lowActiveCost2009,
                    C1v = highActiveCost,
                    C2v = lowActiveCost2009,
                    V1ns = lowActiveVolume,
                    V2ns = lowActiveVolume2009,
                    V1v = highActiveVolume,
                    V2v = lowActiveVolume2009,
                    Tax = EL_CONST * electricityVolume + (2 * lowActiveCost * lowActiveVolume + 2 * highActiveCost * highActiveVolume) +
                          (calculateTaxInPeriod ? 0.03125f * (2 * lowActiveCost2009 * lowActiveCost2009 + 2 * lowActiveCost2009 * lowActiveCost2009)
                              : (2 * lowActiveCost2009 * lowActiveCost2009 + 2 * lowActiveCost2009 * lowActiveCost2009))
                };
                // Create a new instance with updated values instead of modifying the tracked entity
                await _radioService.Create(newRadioCreation);

                Console.Clear();
                Console.WriteLine($"\nПодаток розраховано: {newRadioCreation.Tax}");

            }
            else
            {
                var newRadioCreation = new RadioCreation()
                {
                    OnElectricity = electricityVolume,
                    FactoryId = factoryId,
                    C1ns = lowActiveCost,
                    C2ns = 0,
                    C1v = highActiveCost,
                    C2v = 0,
                    V1ns = lowActiveVolume,
                    V2ns = 0,
                    V1v = highActiveVolume,
                    V2v = 0,
                    Tax = EL_CONST * electricityVolume + (2 * lowActiveCost * lowActiveVolume + 2 * highActiveCost * highActiveVolume)
                };

                Console.Clear();
                await _radioService.Create(newRadioCreation);

                Console.WriteLine($"\nПодаток розраховано: {newRadioCreation.Tax}");

            }

        }
        catch (Exception ex)
        {
            throw new ArgumentNullException("Неправильний ввід", ex);
        }
    }

    public async Task CalculateRadioactiveStorageTax()
    {

        Console.Clear();
        Console.WriteLine("Розрахунок податку за податку за тимчасове зберігання радіоактивних відходів");

        // Отримання списку підприємств
        var factories = _factoryService.GetAllFactories().ToList();
        Console.WriteLine("\nДоступні підприємства:");
        Console.Clear();
        foreach (var factory in factories)
        {
            Console.WriteLine($"{factory.Id}. {factory.Name}");
        }

        Console.Write("\nОберіть ID підприємства: ");
        int factoryId = int.Parse(Console.ReadLine());

        Console.WriteLine(" Виберіть категорію відходів : ");
        Console.WriteLine("1. Високоактивний");
        Console.WriteLine("2. Середньоактивний та низькоактивний");
        Console.WriteLine("3. Високоактивний відходи, представлені у вигляді джерел іонізуючого випромінювання");
        Console.WriteLine(
            "4. Середньоактивний та низькоактивний відходи, представлені у вигляді джерел іонізуючого випромінювання");
        string choise = Console.ReadLine();
        float n;
        switch (choise)
        {
            case "1":
                n = 632539.66f;
                break;
            case "2":
                n = 11807.40f;
                break;
            case "3":
                n = 21084.66f;
                break;
            case "4":
                n = 4216.92f;
                break;
            default:
                throw new ArgumentException("Неправильні дані");
        }

        try
        {
            Console.Write("фактичний об'єм радіоактивних відходів, які зберігаються у виробника таких відходів:");
            float v = float.Parse(Console.ReadLine());
            Console.Write(
                "кількість повних календарних кварталів, протягом яких радіоактивні відходи зберігаються понад установлений особливими умовами ліцензії строк:");
            float t = float.Parse(Console.ReadLine());
            var tempStorage = new TempStorage()
            {
                FactoryId = factoryId,
                T = t,
                N = n,
                V = v,
                Tax = t * n * v

            };
            await _tempStorageServ.Create(tempStorage);
            Console.Clear();

            Console.WriteLine($"\nПодаток розраховано: {tempStorage.Tax}");

        }
        catch (Exception ex)
        {
            throw new ArgumentNullException("Неправильний ввід", ex);
        }
    }
}