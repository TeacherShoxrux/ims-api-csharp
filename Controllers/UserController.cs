namespace imsapi.Controllers;
using System.Threading.Tasks;
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
    
    [HttpPost("Login")]
     public async Task<IActionResult> Authenticate(UserLogin login)
    {
        var session = await _userService.Authenticate(login);
        if(session.IsSuccess){
            return Ok(session);
        }
        return NotFound(session);
        
    }

    [Authorize]
    [HttpPost("Register")]
     public async Task<IActionResult> RegisterUser([FromBody]NewUser newUser)
    {
        var storeId = int.Parse(User.FindFirst("storeId")?.Value!);
        var user = await _userService.CreateUserAsync(storeId, newUser);
        if (!user.IsSuccess)
        {
            return BadRequest(user.ErrorMessage);
        }
        return Ok(user);
    }
   
    [Authorize]
    [HttpGet("info")]
     public async Task<IActionResult> GetInfo()
    {
           var userId =int.Parse(User.FindFirst("userId")?.Value!);
            var user= await _userService.GetUserByIdAsync(userId);
    return Ok(new {user});
    }

    [Authorize]
    [HttpGet("all")]
     public async Task<IActionResult> GetAllUsers()
    {
          var storeId =int.Parse(User.FindFirst("storeId")?.Value!);
         var result= await _userService.GetAllUsersByStoreIdAsync(storeId);
        return Ok(result);
    }
    
    [Authorize]
    [HttpPut]
     public async Task<IActionResult> UpdateUser(UpdateUser newUser)
    {
          var userId =int.Parse(User.FindFirst("userId")?.Value!);
         var result= await _userService.UpdateUserAsync(userId,newUser);
        return Ok(result);
    }
    
}