using imsapi.DTO;
using IMSAPI.DTO.Customer;

namespace imsapi.Services
{
    public interface ICustomerService
    {
        Task<Result<IEnumerable<Customer>>> GetCustomersByStoreIdAsync(int storeId,int pageIndex=1,int pageSize=10);
        Task<Result<IEnumerable<Customer>>> SearchCustomersByStoreIdAsync(int storeId,string searchTerm,int pageIndex=1,int pageSize=10);
        Task<Result<Customer?>> GetCustomerByIdAsync(int id);
        Task<Result<Customer>> AddCustomerAsync(int storeId,int userId,NewCustomer customer);
        Task<Result<Customer>> UpdateCustomerAsync(int userId, Customer customer);
        Task<Result<Customer>> DeleteCustomerAsync(int storeId, int id);
    }
}