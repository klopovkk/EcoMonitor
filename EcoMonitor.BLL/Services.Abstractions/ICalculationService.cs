using EcoMonitor.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoMonitor.BLL.Services.Abstractions
{
    public interface ICalculationService
    {
        public Task AddCalculation(CalculationDTO calculation);
        public Task DeleteCalculation(int id);
        public IEnumerable<CalculationDTO> GetAllCalculations();
        public Task<CalculationDTO> GetCalculationById(int id);
        public Task UpdateCalculation(CalculationDTO calculation);
        public ICollection<CalculationDTO> GetSortedСalculations(int id, int sortArgument);
        public ICollection<CalculationDTO> GetFilteredCalculations(int id, string content);
    }
}
