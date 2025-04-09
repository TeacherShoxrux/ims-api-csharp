using System.Threading.Tasks;
using imsapi.DTO.Category;
using imsapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace imsapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        private readonly ICategoryService _categoryService;
        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var storeId =int.Parse(User.FindFirst("storeId")?.Value);
            var result = await _categoryService.GetCategoriesByStoreId(storeId);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return NotFound(result.ErrorMessage);
        }
       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await _categoryService.GetCategoryById(id);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return NotFound(result.ErrorMessage);
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] NewCategory category)
        {
            var storeId =int.Parse(User.FindFirst("storeId")?.Value);
            var result = await _categoryService.AddCategory(storeId, category);
            return Ok( result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] NewCategory category)
        {
            var result = await _categoryService.UpdateCategory(id, category);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return NotFound(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            return Ok(new { Message = $"Category with ID {id} deleted Hazil" });
        }
    }
}