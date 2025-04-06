namespace imsapi.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public partial class UserController : ControllerBase
{
    // // private readonly IUserService _userService;
    // public UserController(IUserService userService)
    // {
    //     _userService=userService;
    // }
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
    
    [HttpPost("Register")]
     public async Task<IActionResult> RegisterUser(
        // UserRegister register
        )
    {
        // var user = await _userService.RegisterUser(register);
        return Ok("user");
    }
    
}