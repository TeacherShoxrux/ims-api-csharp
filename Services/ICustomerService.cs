using imsapi.DTO;
using IMSAPI.DTO.Customer;

namespace imsapi.Services
{
    public interface ICustomerService
    {
        Task<Result<IEnumerable<Customer>>> GetCustomersByStoreIdAsync(int storeId);
        Task<Result<Customer?>> GetCustomerByIdAsync(int id);
        Task<Result<Customer>> AddCustomerAsync(int userId,Customer customer);
        Task<Result<Customer>> UpdateCustomerAsync(int userId, Customer customer);
        Task<Result<Customer>> DeleteCustomerAsync(int storeId, int id);
    }
}