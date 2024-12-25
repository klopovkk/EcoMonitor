using AutoMapper;
using EcoMonitor.BLL.DTO;
using EcoMonitor.BLL.Services.Abstractions;
using EcoMonitor.Core.Models;
using EcoMonitor.Core.Repository;

namespace EcoMonitor.BLL.Services;

public class PollutionService : IPollutionService
{
    private readonly IRepository<Pollutions> _pollutionRepository;
    private readonly IMapper _mapper;

    public PollutionService(IRepository<Pollutions> pollutionRepository, IMapper mapper)
    {
        _pollutionRepository = pollutionRepository;
        _mapper = mapper;
    }
    public async Task AddPollution(PollutionDTO pollution)
    {
        await _pollutionRepository.Create(_mapper.Map<Pollutions>(pollution));
    }
    public async Task DeletePollution(int id)
    {
        await _pollutionRepository.Delete(id);
    }
    public ICollection<PollutionDTO> GetFilteredPollutions(int id, string content)
    {
        ICollection<Pollutions> pollutions;
        switch (id)
        {
            case 1:
                pollutions = _pollutionRepository.Get().Where(e => e.Name == content).ToList(); ;
                break;
            case 2:
                pollutions = _pollutionRepository.Get().Where(e => e.FlowRate.ToString() == content).ToList();
                break;
            case 3:
                pollutions = _pollutionRepository.Get().Where(e => e.EmissionStandart.ToString() == content).ToList();
                break;
            case 4:
                pollutions = _pollutionRepository.Get().Where(e => e.DangerRate == content).ToList();
                break;
            default:
                throw new ArgumentNullException();
        }
        var pollutionDTOs = _mapper.Map<ICollection<PollutionDTO>>(pollutions);

        return pollutionDTOs;
    }
    public ICollection<PollutionDTO> GetSortedPollutions(int id, int sortArgument)
    {
        ICollection<Pollutions> pollutions;
        switch (id)
        {
            case 1:
                pollutions = sortArgument == 1 ? _pollutionRepository.Get().OrderBy(e => e.Name).ToList()
                    : _pollutionRepository.Get().OrderByDescending(e => e.Name).ToList();
                break;
            case 2:
                pollutions = sortArgument == 1 ? _pollutionRepository.Get().OrderBy(e => e.FlowRate).ToList()
                    : _pollutionRepository.Get().OrderByDescending(e => e.FlowRate).ToList(); break;
            case 3:
                pollutions = sortArgument == 1 ? _pollutionRepository.Get().OrderBy(e => e.EmissionStandart).ToList()
                    : _pollutionRepository.Get().OrderByDescending(e => e.EmissionStandart).ToList(); break;
                break;
            case 4:
                pollutions = sortArgument == 1 ? _pollutionRepository.Get().OrderBy(e => e.DangerRate).ToList()
                    : _pollutionRepository.Get().OrderByDescending(e => e.DangerRate).ToList(); break;
                break;
            default:
                throw new ArgumentNullException();
        }
        var pollutionDTOs = _mapper.Map<ICollection<PollutionDTO>>(pollutions);

        return pollutionDTOs;
    }
    public IEnumerable<PollutionDTO> GetAllPollutions()
    {
        var pollutions = _pollutionRepository.Get();
        var pollutionDTOs = _mapper.Map<IEnumerable<PollutionDTO>>(pollutions);

        return pollutionDTOs;
    }
    public async Task<PollutionDTO> GetPollutionById(int id)
    {
        var pollution = await _pollutionRepository.GetById(id);
        var dto = _mapper.Map<PollutionDTO>(pollution);
        return dto;
    }
    public async Task UpdatePollution(PollutionDTO pollution)
    {
        await _pollutionRepository.Update(_mapper.Map<Pollutions>(pollution));
    }
}