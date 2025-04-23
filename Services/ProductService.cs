namespace imsapi.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using imsapi.Data;
using imsapi.DTO;
using IMSAPI.DTO.Products;
using Microsoft.EntityFrameworkCore;

public class ProductService : IProductService
{
    public ProductService(AppDbContext context)
    {
        _context = context;
    }
    private readonly AppDbContext _context;
    public Task<Result<Product>> AddProductAsync(int storeId, int userId, NewProduct product)
    {
        try
        {
            var productAdded=_context.Products.Add(new(){
                
                storeId = storeId,
                userId = userId,
                name = product.name??"",
                nameLower = product!.name!.ToLower(),
                image = product.image,
                categoryId = product.categoryId,
                description = product.description,
                salePrice = product.salePrice,
                purchasePrice = product.purchasePrice,
                quantity = product.quantity,
                unit=product.unit
            });
            _context.SaveChanges();

            return Task.FromResult(new Result<Product>(true){
                Data = new Product(){
                    id = productAdded.Entity.id,
                    name = product.name,
                    description = product.description,
                    salePrice = product.salePrice,
                    purchasePrice = product.purchasePrice,
                    quantity = product.quantity,
                    image=productAdded.Entity.image
                }
            });
        }
        catch (Exception)
        {
            return Task.FromResult(new Result<Product>(false){
                ErrorMessage = "Error adding product"
            });
        }
    }

    public Task<Result<Product>> DeleteProductAsync(int userId, int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<Product>>> GetAllProductsByStoreIdAndCategoryIdAsync(int storeId, int categoryId, int pageIndex = 1, int pageSize = 10)
    {
        try
        {
            var products = _context.Products.Include(e=>e.Category)
                .Where(p => p.storeId == storeId && p.categoryId == categoryId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Task.FromResult(new Result<List<Product>>(true){
                Data = products.Select(p => new Product(){
                    id = p.id,
                    name = p.name,
                    description = p.description,
                    salePrice = p.salePrice,
                    purchasePrice = p.purchasePrice,
                    categoryName= p.Category?.name??"??",
                    quantity = p.quantity,
                    image=p.image
                }).ToList()
            });
        }
        catch (Exception)
        {
            return Task.FromResult(new Result<List<Product>>(false){
                ErrorMessage = "Error fetching products"
            });
        }
    }

    public Task<Result<List<Product>>> GetAllProductsByStoreIdAndSearchTermAsync(int storeId, string searchTerm, int pageIndex = 1, int pageSize = 10)
    {
        try
        {
            var products = _context.Products
                .Where(p => p.storeId == storeId && p.nameLower.Contains(searchTerm.ToLower()))
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Task.FromResult(new Result<List<Product>>(true){
                Data = products.Select(p => new Product(){
                    id = p.id,
                    name = p.name,
                    description = p.description,
                    salePrice = p.salePrice,
                    purchasePrice = p.purchasePrice,
                    quantity = p.quantity,
                    image=p.image
                }).ToList()
            });
        }
        catch (Exception)
        {
            return Task.FromResult(new Result<List<Product>>(false){
                ErrorMessage = "Error fetching products"
            });
        }
    }

    public Task<Result<List<Product>>> GetAllProductsByStoreIdAsync(int storeId, int pageIndex = 1, int pageSize = 10)
    {
        try
        {
            var products = _context.Products.Include(e=>e.Category)
                .Where(p => p.storeId == storeId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Task.FromResult(new Result<List<Product>>(true){
                Data = products.Select(p => new Product(){
                    id = p.id,
                    name = p.name,
                    description = p.description,
                    salePrice = p.salePrice,
                    purchasePrice = p.purchasePrice,
                    quantity = p.quantity,
                    image=p.image,
                    categoryName=p.Category?.name??""
                }).ToList()
            });
        }
        catch (Exception)
        {
            return Task.FromResult(new Result<List<Product>>(false){
                ErrorMessage = "Error fetching products"
            });
        }
    }

    public Task<Result<Product?>> GetProductByIdAsync(int id)
    {
        try
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return Task.FromResult(new Result<Product?>(false){
                    ErrorMessage = "Product not found"
                });
            }

            return Task.FromResult(new Result<Product?>(true){
                Data = new Product(){
                    id = product.id,
                    name = product.name,
                    description = product.description,
                    salePrice = product.salePrice,
                    purchasePrice = product.purchasePrice,
                    quantity = product.quantity,
                    image=product.image
                }
            });
        }
        catch (Exception)
        {
            return Task.FromResult(new Result<Product?>(false){
                ErrorMessage = "Error fetching product"
            });
        }
    }

    public Task<Result<Product>> UpdateProductAsync(int id, NewProduct product)
    {
        try
        {
            var existingProduct = _context.Products.Find(id);
            if (existingProduct == null)
            {
                return Task.FromResult(new Result<Product>(false){
                    ErrorMessage = "Product not found"
                });
            }

            existingProduct.name = product.name;
            existingProduct.nameLower = product.name.ToLower();
            existingProduct.image = product.image;
            existingProduct.categoryId = product.categoryId;
            existingProduct.description = product.description;
            existingProduct.salePrice = product.salePrice;
            existingProduct.purchasePrice = product.purchasePrice;
            existingProduct.quantity = product.quantity;

            _context.SaveChanges();

            return Task.FromResult(new Result<Product>(true){
                Data = new Product(){
                    id = existingProduct.id,
                    name = existingProduct.name,
                    description = existingProduct.description,
                    salePrice = existingProduct.salePrice,
                    purchasePrice = existingProduct.purchasePrice,
                    quantity = existingProduct.quantity
                }
            });
        }
        catch (Exception)
        {
            return Task.FromResult(new Result<Product>(false){
                ErrorMessage = "Error updating product"
            });
        }
    }
}