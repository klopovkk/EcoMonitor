using AutoMapper;
using EcoMonitor.BLL.DTO;
using EcoMonitor.BLL.Services.Abstractions;
using EcoMonitor.Core.Models;
using EcoMonitor.Core.Repository;

namespace EcoMonitor.BLL.Services;

public class FactoryService : IFactoryService
{
    private readonly IRepository<Factories> _factoryRepository;
    private readonly IMapper _mapper;

    public FactoryService(IRepository<Factories> FactoryRepository, IMapper mapper)
    {
        _factoryRepository = FactoryRepository;
        _mapper = mapper;
    }
    public ICollection<FactoryDTO> GetFilteredFactories(int id, string content)
    {
        ICollection<Factories> factories;
        switch (id)
        {
            case 1:
                factories = _factoryRepository.Get().Where(e => e.Name == content).ToList(); ;
                break;
            case 2:
                factories = _factoryRepository.Get().Where(e => e.Address == content).ToList();
                break;
            case 3:
                factories = _factoryRepository.Get().Where(e => e.Head == content).ToList();
                break;
            default:
                throw new ArgumentNullException();
        }
        var factoryDTOs = _mapper.Map<ICollection<FactoryDTO>>(factories);

        return factoryDTOs;
    }
    public  ICollection<FactoryDTO> GetSortedFactories(int id, int sortArgument)
    {
        ICollection<Factories> factories;
        switch (id)
        {
            case 1:
                factories = sortArgument == 1 ? _factoryRepository.Get().OrderBy(e => e.Name).ToList() 
                    : _factoryRepository.Get().OrderByDescending(e => e.Name).ToList();
                break;
            case 2:
                factories = sortArgument == 1 ? _factoryRepository.Get().OrderBy(e => e.Address).ToList()
                    : _factoryRepository.Get().OrderByDescending(e => e.Address).ToList(); break;
            case 3:
                factories = sortArgument == 1 ? _factoryRepository.Get().OrderBy(e => e.Address).ToList()
                    : _factoryRepository.Get().OrderByDescending(e => e.Head).ToList(); break;
                break;
            default:
                throw new ArgumentNullException();
        }
        var factoryDTOs = _mapper.Map<ICollection<FactoryDTO>>(factories);

        return factoryDTOs;
    }
    public async Task AddFactory(FactoryDTO factory)
    {
        await _factoryRepository.Create(_mapper.Map<Factories>(factory));
    }
    public async Task DeleteFactory(int id)
    {
        await _factoryRepository.Delete(id);
    }
    public ICollection<FactoryDTO> GetAllFactories()
    {
        var factories =  _factoryRepository.Get().ToList();
        var factoryDTOs = _mapper.Map<ICollection<FactoryDTO>>(factories);

        return factoryDTOs;
    }
    public async Task<FactoryDTO> GetFactoryById(int id)
    {
        var factory = await _factoryRepository.GetById(id);
        var dto = _mapper.Map<FactoryDTO>(factory);
        return dto;
    }
    public async Task UpdateFactory(FactoryDTO factory)
    {
        await _factoryRepository.Update(_mapper.Map<Factories>(factory));
    }
}