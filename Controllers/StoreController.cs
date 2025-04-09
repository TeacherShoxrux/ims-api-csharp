namespace imsapi.Controllers;

using imsapi.DTO.Store;
using imsapi.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public partial class StoreController : ControllerBase
{

public StoreController(IStoreService storeService)
{
    _storeService = storeService;
}
private readonly IStoreService _storeService;
[HttpPost("CreateStore")]
public async Task<IActionResult> CreateStore([FromBody] NewStore store)
{
    var result = await _storeService.CreateStoreAsync(store);
    if (result.IsSuccess)
    {
        return Ok(result.Data);
    }
    return BadRequest(result.ErrorMessage);
}
[HttpGet("GetStore")]
public async Task<IActionResult> GetStore()
{
    var storeId =int.Parse(User.FindFirst("storeId")?.Value);
    var result = await _storeService.GetStoreByIdAsync(storeId);
    if (result.IsSuccess)
    {
        return Ok(result.Data);
    }
    return NotFound(result.ErrorMessage);
}
[HttpGet("GetAllStores")]
public async Task<IActionResult> GetAllStores()
{
    var result = await _storeService.GetAllStoresAsync();
    if (result.IsSuccess)
    {
        return Ok(result.Data);
    }
    return NotFound(result.ErrorMessage);
}
}