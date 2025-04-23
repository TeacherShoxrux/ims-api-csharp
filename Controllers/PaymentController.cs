using System.Threading.Tasks;
using imsapi.DTO;
using imsapi.DTO.Payment;
using imsapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace imsapi.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService=paymentService;
        
    }
    [HttpPost("Create")]

    public async Task<IActionResult> CreatePayment(NewPayment payment)
    {
        try
        {
             var storeId =int.Parse(User.FindFirst("storeId")?.Value);
             var userId =int.Parse(User.FindFirst("userId")?.Value);
            var result= await _paymentService.ProcessPaymentAsync(storeId,userId,payment);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound(result);
            
        }
        catch (System.Exception e)
        {
           return NoContent();
        }
    }
    [HttpGet("GetPayments")]
    public async Task<IActionResult> GetPaymentsListPagenatedByStoreId( [FromQuery]int page=1, [FromQuery]int pageSize=10)
    {
        try
        {
            var storeId =int.Parse(User.FindFirst("storeId")?.Value);
            var result= await _paymentService.GetPaymentsListPagenatedByStoreId(storeId,page,pageSize);
            return Ok(result);
        }
        catch (Exception e)
        {
            
            return BadRequest(new Result<List<PaymentShort>>(false)
            {
                ErrorMessage = $"An error occurred while fetching the payments list. -> {e.Message}"
            });
        }
    }

    [HttpGet("GetPaymentById/{id}")]
    [ProducesResponseType(typeof(Result<Payment>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<Payment>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
   public async Task<IActionResult> GetPaymentById(int id)
    {
        try
        {
            var result= await _paymentService.GetPaymentById(id);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new Result<Payment>(false)
            {
                ErrorMessage = "Payment not found."
            });
        }
        catch (Exception ex)
        {
            
            return BadRequest(new Result<Payment>(false)
            {
                ErrorMessage = "An error occurred while fetching the payment."
            });
        }
    }
  
}