using EcoMonitor.BLL.DTO;
using EcoMonitor.BLL.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace EcoMonitorWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiskController : ControllerBase
    {
        IRiskService _riskService;

        public RiskController(IRiskService riskService)
        {
            _riskService = riskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRisks()
        {
            try
            {
                var risk = _riskService.GetAllRisks();
                return Ok(risk);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRiskById(int id)
        {
            try
            {
                var risk = await _riskService.GetRiskById(id);
                return Ok(risk);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostRisk(RiskDTO risk)
        {
            try
            {
                await _riskService.AddRisk(risk);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutRisk(RiskDTO risk)
        {
            try
            {
                await _riskService.UpdateRisk(risk);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRisk(int id)
        {
            try
            {
                await _riskService.DeleteRisk(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
