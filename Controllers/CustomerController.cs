using imsapi.Services;
using IMSAPI.DTO.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace imsapi.Controllers
{
   

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] NewCustomer customer)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value);
            var storeId =int.Parse(User.FindFirst("storeId")?.Value);
            var result = await _customerService.AddCustomerAsync(storeId,userId,customer);
            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(GetCustomer), new { id = result.Data.id }, result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var result = await _customerService.GetCustomerByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return NotFound(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var storeId = 1; // Replace with actual store ID
            var result = await _customerService.DeleteCustomerAsync(storeId,id);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return NotFound(result.ErrorMessage);
        }
    }
}
    