namespace imsapi.Controllers;

using imsapi.DTO.Store;
using imsapi.Services;
using IMSAPI.DTO.Products;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private IProductService _productService;

    public ProductController(IProductService productService)
    {
    _productService = productService;
    }
    [HttpGet("GetAllProductsByStoreId/{storeId}")]
    public async Task<IActionResult> GetAllProductsByStoreId(int storeId, int pageIndex = 1, int pageSize = 10)
    {
        var result = await _productService.GetAllProductsByStoreIdAsync(storeId, pageIndex, pageSize);
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        return NotFound(result.ErrorMessage);
    }
    [HttpGet("GetAllProductsByStoreIdAndCategoryId/{storeId}/{categoryId}")]
    public async Task<IActionResult> GetAllProductsByStoreIdAndCategoryId(int storeId, int categoryId, int pageIndex = 1, int pageSize = 10)
    {
        var result = await _productService.GetAllProductsByStoreIdAndCategoryIdAsync(storeId, categoryId, pageIndex, pageSize);
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        return NotFound(result.ErrorMessage);
    }
    [HttpGet("GetAllProductsByStoreIdAndSearchTerm/{storeId}/{searchTerm}")]
    public async Task<IActionResult> GetAllProductsByStoreIdAndSearchTerm(int storeId, string searchTerm, int pageIndex = 1, int pageSize = 10)
    {
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
    [HttpPost("AddProduct/{storeId}")]
    public async Task<IActionResult> AddProduct(int storeId, [FromBody] NewProduct product)
    {
        var result = await _productService.AddProductAsync(storeId, 1, product);
        if (result.IsSuccess)
        {
            return Created("", result.Data);
        }
        return NotFound(result.ErrorMessage);
    }
    
}