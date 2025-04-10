namespace imsapi.Controllers
{
    using imsapi.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatiscsService _statisticsService;

        public StatisticsController(IStatiscsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet("GetStoreTotal")]
        public async Task<IActionResult> GetStoreTotalById()
        {
            var storeId =int.Parse(User.FindFirst("storeId")?.Value);
            var result = await _statisticsService.GetStoreTotalByIdAsync(storeId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound(result.ErrorMessage);
        }
        [HttpGet("GetStore")] 
        public async Task<IActionResult> GetStoreById(DateTime startDate, DateTime endDate)
        {
            var storeId =int.Parse(User.FindFirst("storeId")?.Value);
            var result = await _statisticsService.GetStoreByIdAsync(storeId, startDate, endDate);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound(result.ErrorMessage);
        }
    }
}