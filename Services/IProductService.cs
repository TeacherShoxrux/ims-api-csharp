using imsapi.DTO;
using IMSAPI.DTO.Products;

namespace imsapi.Services
{
    public interface IProductService
    {
        Task<Result<List<Product>>> GetAllProductsByStoreIdAsync(int storeId,int pageIndex=1, int pageSize=10);
        Task<Result<List<Product>>> GetAllProductsByStoreIdAndCategoryIdAsync(int storeId, int categoryId, int pageIndex=1, int pageSize=10);
        Task<Result<List<Product>>> GetAllProductsByStoreIdAndSearchTermAsync(int storeId, string searchTerm, int pageIndex=1, int pageSize=10);
        Task<Result<Product?>> GetProductByIdAsync(int id);
        Task<Result<Product>> AddProductAsync(int storeId,int userId,NewProduct product);
        Task<Result<Product>> UpdateProductAsync(int id, NewProduct product);
        Task<Result<Product>> DeleteProductAsync(int userId,int id);
    }
}