using AutoMapper;
using EcoMonitor.BLL.DTO;
using EcoMonitor.BLL.Services.Abstractions;
using EcoMonitor.Core.Models;
using EcoMonitor.Core.Repository;

namespace EcoMonitor.BLL.Services;

public class CalculationService : ICalculationService
{
    private readonly IRepository<Calculations> _calculationRepository;
    private readonly IMapper _mapper;

    public CalculationService(IRepository<Calculations> calculationRepository, IMapper mapper)
    {
        _calculationRepository = calculationRepository;
        _mapper = mapper;
    }
    public async Task AddCalculation(CalculationDTO calculation)
    {
        await _calculationRepository.Create(_mapper.Map<Calculations>(calculation));
    }
    public async Task DeleteCalculation(int id)
    {
        await _calculationRepository.Delete(id);
    }
    public IEnumerable<CalculationDTO> GetAllCalculations()
    {
        var calculations = _calculationRepository.Get();
        var calculationDTOs = _mapper.Map<IEnumerable<CalculationDTO>>(calculations);

        return calculationDTOs;
    }
    public ICollection<CalculationDTO> GetFilteredCalculations(int id, string content)
    {
        ICollection<Calculations> calculations;
        switch (id)
        {
            case 1:
                calculations = _calculationRepository.Get().Where(e => e.GeneralEmission.ToString() == content).ToList(); ;
                break;
            case 2:
                calculations = _calculationRepository.Get().Where(e => e.Date == content).ToList();
                break;
            case 3:
                calculations = _calculationRepository.Get().Where(e => e.PollutionId.ToString() == content).ToList();
                break;
            case 4:
                calculations = _calculationRepository.Get().Where(e => e.FactoryId.ToString() == content).ToList();
                break;
            default:
                throw new ArgumentNullException();
        }
        var CalculationDTOs = _mapper.Map<ICollection<CalculationDTO>>(calculations);

        return CalculationDTOs;
    }
    public ICollection<CalculationDTO> GetSortedСalculations(int id, int sortArgument)
    {
        ICollection<Calculations> calculations;
        switch (id)
        {
            case 1:
                calculations = sortArgument == 1 ? _calculationRepository.Get().OrderBy(e => e.GeneralEmission).ToList()
                    : _calculationRepository.Get().OrderByDescending(e => e.GeneralEmission).ToList();
                break;
            case 2:
                calculations = sortArgument == 1 ? _calculationRepository.Get().OrderBy(e => e.Date).ToList()
                    : _calculationRepository.Get().OrderByDescending(e => e.Date).ToList(); break;
            case 3:
                calculations = sortArgument == 1 ? _calculationRepository.Get().OrderBy(e => e.PollutionId).ToList()
                    : _calculationRepository.Get().OrderByDescending(e => e.PollutionId).ToList(); break;
            default:
                throw new ArgumentNullException();
        }
        var CalculationDTOs = _mapper.Map<ICollection<CalculationDTO>>(calculations);

        return CalculationDTOs;
    }
    public async Task<CalculationDTO> GetCalculationById(int id)
    {
        var calculation = await _calculationRepository.GetById(id);
        var dto = _mapper.Map<CalculationDTO>(calculation);
        return dto;
    }
    public async Task UpdateCalculation(CalculationDTO calculation)
    {
        await _calculationRepository.Update(_mapper.Map<Calculations>(calculation));
    }
}