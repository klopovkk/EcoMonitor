using AutoMapper;
using EcoMonitor.BLL.DTO;
using EcoMonitor.Core.Models;
using EcoMonitor.Core.Repository;

namespace EcoMonitor.BLL.Services.Abstractions;

public interface IPollutionService
{
    public Task AddPollution(PollutionDTO pollution);
    public Task DeletePollution(int  id);
    public IEnumerable<PollutionDTO> GetAllPollutions();
    public Task<PollutionDTO> GetPollutionById(int id);
    public  Task UpdatePollution(PollutionDTO pollution);
    public ICollection<PollutionDTO> GetSortedPollutions(int id, int sortArgument);
    public ICollection<PollutionDTO> GetFilteredPollutions(int id, string content);
}