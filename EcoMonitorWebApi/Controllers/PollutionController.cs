using EcoMonitor.BLL.DTO;
using EcoMonitor.BLL.Services;
using EcoMonitor.BLL.Services.Abstractions;
using EcoMonitor.Core.Models;
using EcoMonitor.Core.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcoMonitorWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollutionController : ControllerBase
    {
        IPollutionService _pollutionService;

        public PollutionController(IPollutionService pollutionService)
        {
            _pollutionService = pollutionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPollutions()
        {
            try
            {
                var pollution = _pollutionService.GetAllPollutions();
                return Ok(pollution);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPollutionById(int id)
        {
            try
            {
                var pollution = await _pollutionService.GetPollutionById(id);
                return Ok(pollution);
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
                var factories = _pollutionService.GetFilteredPollutions(id, content);
                return Ok(factories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpGet("{id}/pollutions/sorted/{sortArgument}")]
        public IActionResult GetSortedFactories(int id, int sortArgument)
        {
            try
            {
                var factories = _pollutionService.GetSortedPollutions(id, sortArgument);
                return Ok(factories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostPollution(PollutionDTO pollution)
        {
            try
            {
                await _pollutionService.AddPollution(pollution);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutPollution(PollutionDTO pollution)
        {
            try
            {
                await _pollutionService.UpdatePollution(pollution);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePollution(int id)
        {
            try
            {
                await _pollutionService.DeletePollution(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}

