namespace imsapi.Controllers;

using imsapi.Services;
using IMSAPI.DTO.User;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public partial class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService=userService;
    }
    [HttpGet]
     public async Task<IActionResult> Authenticate(int id)
    {
        // var session = await _userService.GetUserDetails(id);
        return Ok("session");
    }
    
    [HttpPost("Login")]
     public async Task<IActionResult> Authenticate(
        // UserLogin login
        )
    {
        // var session = await _userService.Authenticate(login);
        return Ok("session");
    }
    
    [HttpPost("Register/{storeId}")]
     public async Task<IActionResult> RegisterUser(int storeId,[FromBody]NewUser newUser)
    {
        var user = await _userService.CreateUserAsync(storeId, newUser);
        if (!user.IsSuccess)
        {
            return BadRequest(user.ErrorMessage);
        }
        return Ok(user.Data);
        
    }
    
}