using AutoMapper;
using EcoMonitor.BLL.DTO;
using EcoMonitor.BLL.Services.Abstractions;
using EcoMonitor.Core.Models;
using EcoMonitor.Core.Repository;

namespace EcoMonitor.BLL.Services;

public class RiskService : IRiskService
{
    private readonly IRepository<Risk> _riskRepository;
    private readonly IRepository<Factories> _factoryRepository;
    private readonly IRepository<Pollutions> _pollRepository;

    private readonly IMapper _mapper;

    public RiskService(IRepository<Risk> riskRepository, IRepository<Pollutions> pollRepository, IRepository<Factories> factoryRepository, IMapper mapper)
    {
        _pollRepository = pollRepository;
        _factoryRepository = factoryRepository;
        _riskRepository = riskRepository;
        _mapper = mapper;
    }
    public async Task AddRisk(RiskDTO risk)
    {
        await _riskRepository.Create(_mapper.Map<Risk>(risk));
    }
    public async Task DeleteRisk(int id)
    {
        await _riskRepository.Delete(id);
    }
    public IEnumerable<RiskDTO> GetAllRisks()
    {
        var risks = _riskRepository.Get();
        var riskDTOs = _mapper.Map<IEnumerable<RiskDTO>>(risks);
        foreach (var dto in riskDTOs)
        {
            dto.FactoryName = _factoryRepository.GetById(dto.FactoryId).Result.Name;
            dto.PollutionName = _pollRepository.GetById(dto.PollutionId).Result.Name;

        }
        return riskDTOs;
    }
    public async Task<RiskDTO> GetRiskById(int id)
    {
        var risk = await _riskRepository.GetById(id);
        var dto = _mapper.Map<RiskDTO>(risk);
        return dto;
    }
    public async Task UpdateRisk(RiskDTO risk)
    {
        await _riskRepository.Update(_mapper.Map<Risk>(risk));
    }
}