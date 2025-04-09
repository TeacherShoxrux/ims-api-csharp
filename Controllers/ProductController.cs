namespace imsapi.Controllers;

using imsapi.DTO.Store;
using imsapi.Services;
using IMSAPI.DTO.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductController : ControllerBase
{
    private IProductService _productService;

    public ProductController(IProductService productService)
    {
    _productService = productService;
    }
    [HttpGet("GetAllProductsByStoreId/")]
    public async Task<IActionResult> GetAllProductsByStoreId( int pageIndex = 1, int pageSize = 10)
    {
          
        var storeId =int.Parse(User.FindFirst("storeId")?.Value);
        var result = await _productService.GetAllProductsByStoreIdAsync(storeId, pageIndex, pageSize);
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        return NotFound(result.ErrorMessage);
    }
    [HttpGet("GetAllProductsByStoreIdAndCategoryId/{categoryId}")]
    public async Task<IActionResult> GetAllProductsByStoreIdAndCategoryId(int categoryId, int pageIndex = 1, int pageSize = 10)
    {
        
        var storeId =int.Parse(User.FindFirst("storeId")?.Value);
        var result = await _productService.GetAllProductsByStoreIdAndCategoryIdAsync(storeId, categoryId, pageIndex, pageSize);
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        return NotFound(result.ErrorMessage);
    }
    [HttpGet("GetAllProductsByStoreIdAndSearchTerm/{searchTerm}")]
    public async Task<IActionResult> GetAllProductsByStoreIdAndSearchTerm(string searchTerm, int pageIndex = 1, int pageSize = 10)
    {
 
        var storeId =int.Parse(User.FindFirst("storeId")?.Value);
        var result = await _productService.GetAllProductsByStoreIdAndSearchTermAsync(storeId, searchTerm, pageIndex, pageSize);
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        return NotFound(result.ErrorMessage);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var result = await _productService.GetProductByIdAsync(id);
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        return NotFound(result.ErrorMessage);
    }
    [HttpPost("AddProduct")]
    public async Task<IActionResult> AddProduct([FromBody] NewProduct product)
    {
          
        var storeId =int.Parse(User.FindFirst("storeId")?.Value);
        var userId = int.Parse(User.FindFirst("userId")?.Value);
        var result = await _productService.AddProductAsync(storeId, userId, product);
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        return NotFound(result.ErrorMessage);
    }
    
}