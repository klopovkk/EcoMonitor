using EcoMonitor.BLL.DTO;
using EcoMonitor.BLL.Services;
using EcoMonitor.BLL.Services.Abstractions;
using EcoMonitor.Core.Models;
using EcoMonitor.Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EcoMonitorWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculationController : ControllerBase
    {
        ICalculationService _calculationRepository;

        public CalculationController(ICalculationService calculationService)
        {
            _calculationRepository = calculationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFactories()
        {
            try
            {
                var calculation = _calculationRepository.GetAllCalculations();
                return Ok(calculation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostFactories(CalculationDTO calculation)
        {
            try
            {
                await _calculationRepository.AddCalculation(calculation);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCalculationById(int id)
        {
            try
            {
                var calculation = await _calculationRepository.GetCalculationById(id);
                return Ok(calculation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpGet("{id}/filtered")]
        public async Task<IActionResult> GetFactories(int id, string content)
        {
            try
            {
                var factories = _calculationRepository.GetFilteredCalculations(id, content);
                return Ok(factories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpGet("{id}/calculation/sorted/{sortArgument}")]
        public IActionResult GetSortedFactories(int id, int sortArgument)
        {
            try
            {
                var factories = _calculationRepository.GetSortedСalculations(id, sortArgument);
                return Ok(factories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpPut]
        public async Task<IActionResult> PutCalculation(CalculationDTO calculation)
        {
            try
            {
                await _calculationRepository.UpdateCalculation(calculation);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCalculation(int id)
        {
            try
            {
                await _calculationRepository.DeleteCalculation(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
