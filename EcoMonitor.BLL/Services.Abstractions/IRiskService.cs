using EcoMonitor.BLL.DTO;

namespace EcoMonitor.BLL.Services.Abstractions;

public interface IRiskService
{
    public Task AddRisk(RiskDTO risk);
    public Task DeleteRisk(int id);
    public IEnumerable<RiskDTO> GetAllRisks();
    public Task<RiskDTO> GetRiskById(int id);
    public Task UpdateRisk(RiskDTO risk);
}