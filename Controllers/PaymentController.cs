using System.Threading.Tasks;
using imsapi.DTO;
using imsapi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace imsapi.Controllers;
[ApiController]
[Route("api/[controller]")]
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
            var result= await _paymentService.ProcessPaymentAsync(1,1,payment);
            return Ok(result);
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
    
}