using EcoMonitor.BLL.DTO;

namespace EcoMonitor.BLL.Services.Abstractions;

public interface IFactoryService
{
    public Task AddFactory(FactoryDTO factory);
    public Task DeleteFactory(int id);
    public ICollection<FactoryDTO> GetAllFactories();
    public Task<FactoryDTO> GetFactoryById(int id);
    public Task UpdateFactory(FactoryDTO factory);

    public ICollection<FactoryDTO> GetFilteredFactories(int id, string content);
    public ICollection<FactoryDTO> GetSortedFactories(int id, int sortArgument);
}