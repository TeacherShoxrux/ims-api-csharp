using System.Threading.Tasks;
using imsapi.DTO.Category;
using imsapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace imsapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        private readonly ICategoryService _categoryService;
        [HttpGet("GetCategoriesByStoreId/{storeId}")]
        public async Task<IActionResult> GetAllCategories(int storeId)
        {
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

        [HttpPost("CreateCategory/{storeId}")]
        [ProducesResponseType(typeof(NewCategory), 201)]
        public async Task<IActionResult> CreateCategory(int storeId,[FromBody] NewCategory category)
        {
            var result = await _categoryService.AddCategory(storeId, category);
            return Created("", result.Data);
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