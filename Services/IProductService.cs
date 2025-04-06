using imsapi.DTO;
using IMSAPI.DTO.Products;

namespace imsapi.Services
{
    public interface IProductService
    {
        Task<Result<List<Product>>> GetAllProductsByStoreIdAsync(int storeId);
        Task<Result<Product?>> GetProductByIdAsync(int id);
        Task<Result<Product>> AddProductAsync(int storeId,int userId,NewProduct product);
        Task<Result<Product>> UpdateProductAsync(int id, Product product);
        Task<Result<Product>> DeleteProductAsync(int userId,int id);
    }
}