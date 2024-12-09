using EcoMonitor.BLL.DTO;
using EcoMonitor.BLL.Services.Abstractions;
using EcoMonitor.Core.Models;
using EcoMonitor.Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EcoMonitorWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactoryController : ControllerBase
    {
        IFactoryService _factoryService;
        public FactoryController(IFactoryService factoryService)
        {
            _factoryService = factoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetFactories()
        {
            try
            {
                var Factories =  _factoryService.GetAllFactories();
                return Ok(Factories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFactoryById(int id)
        {
            try
            {
                var factories = await _factoryService.GetFactoryById(id);
                return Ok(factories);
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
                var factories = _factoryService.GetFilteredFactories(id, content);
                return Ok(factories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpGet("{id}/factories/sorted/{sortArgument}")]
        public IActionResult GetSortedFactories(int id, int sortArgument)
        {
            try
            {
                var factories = _factoryService.GetSortedFactories(id, sortArgument);
                return Ok(factories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostFactories(FactoryDTO factory)
        {
            try
            {
                await _factoryService.AddFactory(factory);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpPut]
        public async Task<IActionResult> PutFactory(FactoryDTO factory)
        {
            try
            {
                await _factoryService.UpdateFactory(factory);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteFactory(int id)
        {
            try
            {
                await _factoryService.DeleteFactory(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
