namespace imsapi.Controllers
{
    using imsapi.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    // [Authorize]
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
        [Authorize]
        public async Task<IActionResult> GetStoreTotalById()
        {
           
            // var storeId =1;//l int.Parse(User.FindFirst("storeId")?.Value);
             var storeId =int.Parse(User.FindFirst("storeId")?.Value);
            var result = await _statisticsService.GetStoreTotalByIdAsync(storeId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound(result.ErrorMessage);
            
            
        }
        [HttpGet("GetStore")] 
        [Authorize]
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
        
        [HttpGet("export-products")] 
        [Authorize]
        public async Task<IActionResult> GetStoreById()
        {
            var storeId =int.Parse(User.FindFirst("storeId")?.Value);
            var result = await _statisticsService.ExportStoreTotalByIdAsync(storeId);
            if (result.IsSuccess)
            {
                
                return File(result?.Data?.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "products.xlsx");
            }
            return NoContent();
        }
        [HttpGet("export-sale-month")] 
        [Authorize]
        public async Task<IActionResult> GetThisMonthId()
        {
            var storeId =int.Parse(User.FindFirst("storeId")?.Value);
            var result = await _statisticsService.ExportThisMonthSaleTotalByIdAsync(storeId);
            if (result.IsSuccess)
            {
                
                return File(result?.Data?.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "products.xlsx");
            }
            return NoContent();
        }
        [HttpGet("export-sale-week")] 
        [Authorize]
        public async Task<IActionResult> GetThisWeek()
        {
            var storeId =int.Parse(User.FindFirst("storeId")?.Value);
            var result = await _statisticsService.ExportThisWeekSaleTotalByIdAsync(storeId);
            if (result.IsSuccess)
            {
                
                return File(result?.Data?.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "products.xlsx");
            }
            return NoContent();
        }   
            [HttpGet("export-sale-day")] 
        [Authorize]
        public async Task<IActionResult> GetThisDay()
        {
            var storeId =int.Parse(User.FindFirst("storeId")?.Value);
            var result = await _statisticsService.ExportThisDaySaleTotalByIdAsync(storeId);
            if (result.IsSuccess)
            {
                
                return File(result?.Data?.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "products.xlsx");
            }
            return NoContent();
        }
        

    }
}