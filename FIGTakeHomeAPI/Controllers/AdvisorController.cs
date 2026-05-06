using FIGTakeHomeAPI.DTOs;
using FIGTakeHomeAPI.Models;
using FIGTakeHomeAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FIGTakeHomeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdvisorController : ControllerBase
    {
        private readonly AdvisorService _advisorService;

        public AdvisorController(AdvisorService advisorService)
        {
            _advisorService = advisorService;
        }

        [HttpGet("summaries")]
        public async Task<ActionResult<AdvisorSummariesResponseDto>> GetSummaries()
        {
            var summaries = await _advisorService.GetAdvisorSummariesAsync();
            return Ok(summaries);
        }

        [HttpGet("{advisorId}/carrier-distribution")]
        public async Task<ActionResult<CarrierDistributionDto>> GetCarrierDistribution(int advisorId)
        {
            var distribution = await _advisorService.GetCarrierDistributionAsync(advisorId);
            if (distribution.AdvisorId == 0)
                return NotFound();
            return Ok(distribution);
        }

        [HttpGet("{id}/monthlytrend")]
        public async Task<ActionResult<Statement>> GetMonthltTrendAsync(int id)
        {
            var summaries = await _advisorService.GetAdvisorMonthlyTrend(id);
            return Ok(summaries);
        }
    }
}
