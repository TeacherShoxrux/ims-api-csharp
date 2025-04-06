using imsapi.DTO;
using imsapi.DTO.Category;

namespace imsapi.Services
{
    public interface ICategoryService
    {
        // Define methods for the category service
        Task<Result<IEnumerable<Category>>> GetCategoriesByStoreId(int storeId);
        Task<Result<Category>> GetCategoryById(int id);
        Task<Result<Category>> AddCategory(NewCategory category);
        Task<Result<Category>> UpdateCategory(int categoryId, NewCategory category);
        Task<Result<Category>> DeleteCategory(int categoryId);
    }
}