namespace EcoMonitor.UI.Tax.Abstraction;

public interface ITaxCalculator
{
    public Task CalculateEmissionTax();
    public Task CalculateWaterPollutionTax();
    public Task CalculateWasteTax();
    public Task CalculateRadioactiveWasteTax();
    public Task CalculateRadioactiveStorageTax();
    public Task CalculateRiskAsync();
}