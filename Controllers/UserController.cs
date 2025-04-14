namespace imsapi.Controllers;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using global::DTO.User;
using imsapi.Services;
using IMSAPI.DTO.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public partial class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserController(IUserService userService,IHttpContextAccessor httpContextAccessor)
    {
        _userService=userService;
         _httpContextAccessor = httpContextAccessor;
    }
    [HttpGet]
     public async Task<IActionResult> Authenticate(int id)
    {
        // var session = await _userService.GetUserDetails(id);
        return Ok("session");
    }
    
    [HttpPost("Login")]
     public async Task<IActionResult> Authenticate(UserLogin login)
    {
        var session = await _userService.Authenticate(login);
        return Ok(session);
    }

    [Authorize()]
    [HttpPost("Register")]
     public async Task<IActionResult> RegisterUser([FromBody]NewUser newUser)
    {
        var storeId = int.Parse(User.FindFirst("storeId")?.Value);
        var user = await _userService.CreateUserAsync(storeId, newUser);
        if (!user.IsSuccess)
        {
            return BadRequest(user.ErrorMessage);
        }
        return Ok(user);
    }
   
    [Authorize]
    [HttpGet("info")]
     public async Task<IActionResult> getInfo()
    {
            var userId = User.FindFirst("userId")?.Value;
            var role = User.FindFirst("userRole")?.Value;
            var storeId = User.FindFirst("storeId")?.Value;
    return Ok(new { 
        UserId = userId, 
        Role = role, 
        StoreId = storeId,
      });
    }

    
}