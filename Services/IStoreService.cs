using imsapi.DTO;
using imsapi.DTO.Store;
using IMSAPI.DTO.Store;

namespace imsapi.Services
{
    public interface IStoreService
    {
        Task<Result<List<Store>>> GetAllStoresAsync();
        Task<Result<Store?>> GetStoreByIdAsync(int id);
        Task<Result<Store>> CreateStoreAsync(NewStore store);
        Task<Result<Store>> UpdateStoreAsync(int id, NewStore store);
        Task<Result<Store>> DeleteStoreAsync(int id);
    }
}