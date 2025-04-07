using imsapi.Data;
using imsapi.DTO;
using imsapi.DTO.Category;

namespace imsapi.Services
{
    public class CategoryService : ICategoryService
    {
        public CategoryService(AppDbContext context)
        {
           _context= context;
        }
        private readonly AppDbContext _context;
        public Task<Result<IEnumerable<Category>>> GetCategoriesByStoreId(int storeId)
        {
            try
            {
                var categories = _context.Categories.Where(c => c.storeId == storeId).ToList();
                return Task.FromResult(new Result<IEnumerable<Category>>(true)
                {
                    
                    Data = categories.Select(c => new Category()
                    {
                        id = c.id,
                        name = c.name,
                        storeId = c.storeId,
                        createdAt = c.createdAt,
                        updatedAt = c.updatedAt
                        
                    }).ToList()
                });
                
            }
            catch (Exception ex)
            {
                return Task.FromResult(new Result<IEnumerable<Category>>(ex.Message));
            }
        }

        public Task<Result<Category>> GetCategoryById(int id)
        {
            try
            {
                var category = _context.Categories.FirstOrDefault(c => c.id == id);
                if (category == null)
                {
                    return Task.FromResult(new Result<Category>("Category not found"));
                }

                return Task.FromResult(new Result<Category>(true)
                {
                    Data = new Category()
                    {
                        id = category.id,
                        name = category.name,
                        storeId = category.storeId,
                        createdAt = category.createdAt,
                        updatedAt = category.updatedAt
                    }
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new Result<Category>(ex.Message));
            }
        }

        public Task<Result<Category>> AddCategory(int storeId,NewCategory category)
        {
            try
            {
                var newCategory = _context.Categories.Add(new()
                {
                    name = category.name,
                    storeId = storeId,
                    description = category.description,
                });
                _context.SaveChanges();
                return Task.FromResult(new Result<Category>(true)
                {
                    Data = new Category()
                    {
                        id = newCategory.Entity.id,
                        name = newCategory.Entity.name,
                        storeId = newCategory.Entity.storeId,
                        createdAt = newCategory.Entity.createdAt,
                        updatedAt = newCategory.Entity.updatedAt
                    }
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new Result<Category>(ex.Message));
            }
        }

        public Task<Result<Category>> UpdateCategory(int categoryId, NewCategory category)
        {
            try
            {
                var existingCategory = _context.Categories.FirstOrDefault(c => c.id == categoryId);
                if (existingCategory == null)
                {
                    return Task.FromResult(new Result<Category>("Category not found"));
                }

                existingCategory.name = category.name;
                existingCategory.description = category.description;
                _context.SaveChanges();

                return Task.FromResult(new Result<Category>(true)
                {
                    Data = new Category()
                    {
                        id = existingCategory.id,
                        name = existingCategory.name,
                        storeId = existingCategory.storeId,
                        createdAt = existingCategory.createdAt,
                        updatedAt = existingCategory.updatedAt
                    }
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new Result<Category>(ex.Message));
            }
        }

        public Task<Result<Category>> DeleteCategory(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
 
}