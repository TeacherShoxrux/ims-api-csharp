using imsapi.Data;
using imsapi.DTO;
using IMSAPI.DTO.Customer;

namespace imsapi.Services
{


    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;
        public CustomerService(AppDbContext context)
        {
            _context = context;
        }

        public Task<Result<Customer>> AddCustomerAsync(int storeId,int userId, NewCustomer customer)
        {
            try
            {
                

              var newCustomer=  _context.Customers.Add(new(){
                    fullName = customer.name,
                    phone = customer.phone,
                    storeId = storeId,
                    userId = userId,
                    info = customer.info,
                    createdAt = DateTime.UtcNow});
                _context.SaveChanges();

                return Task.FromResult<Result<Customer>>(new Result<Customer>(true){
                    Data = new Customer(){
                        id = newCustomer.Entity.id,
                        name = newCustomer.Entity.fullName,
                        phone = newCustomer.Entity.phone,
                        createdAt = newCustomer.Entity.createdAt
                    }
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new Result<Customer>("Error adding customer: " + ex.Message));
            }
        }

        public Task<Result<Customer>> DeleteCustomerAsync(int storeId, int id)
        {
            try{
                var customer = _context.Customers.FirstOrDefault(c => c.id == id && c.storeId == storeId);
                if (customer == null)
                {
                    return Task.FromResult(new Result<Customer>("Customer not found"));
                }

                _context.Customers.Remove(customer);
                _context.SaveChanges();

                return Task.FromResult(new Result<Customer>(true){
                    Data = new Customer(){
                        id = customer.id,
                        name = customer.fullName,
                        phone = customer.phone,
                        createdAt = customer.createdAt
                    }
                });

            }catch (Exception ex)
            {
                return Task.FromResult(new Result<Customer>("Error deleting customer: " + ex.Message));
            }
        }

        public Task<Result<Customer?>> GetCustomerByIdAsync(int id)
        {
            try
            {
                var customer = _context.Customers.FirstOrDefault(c => c.id == id);
                if (customer == null)
                {
                    return Task.FromResult(new Result<Customer?>("Customer not found"));
                }

                return Task.FromResult(new Result<Customer?>(false){
                    Data = new Customer(){
                        id = customer.id,
                        name = customer.fullName,
                        phone = customer.phone,
                        createdAt = customer.createdAt
                    }
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new Result<Customer?>("Error retrieving customer: " + ex.Message));
            }
        }

        public Task<Result<IEnumerable<Customer>>> GetCustomersByStoreIdAsync(int storeId)
        {
            try
            {
                var customers = _context.Customers.Where(c => c.storeId == storeId).ToList();
                if (customers == null || !customers.Any())
                {
                    return Task.FromResult(new Result<IEnumerable<Customer>>("No customers found"));
                }

                var customerList = customers.Select(c => new Customer()
                {
                    id = c.id,
                    name = c.fullName,
                    phone = c.phone,
                    createdAt = c.createdAt
                });

                return Task.FromResult(new Result<IEnumerable<Customer>>(true){
                    Data = customerList
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new Result<IEnumerable<Customer>>("Error retrieving customers: " + ex.Message));
            }
        }

        public Task<Result<Customer>> UpdateCustomerAsync(int userId, Customer customer)
        {
        try{
                var existingCustomer = _context.Customers.FirstOrDefault(c => c.id == customer.id);
                if (existingCustomer == null)
                {
                    return Task.FromResult(new Result<Customer>("Customer not found"));
                }
                existingCustomer.fullName = customer.name;
                existingCustomer.phone = customer.phone;
                existingCustomer.userId = userId;

                _context.SaveChanges();

                return Task.FromResult(new Result<Customer>(true){
                    Data = new Customer(){
                        id = existingCustomer.id,
                        name = existingCustomer.fullName,
                        phone = existingCustomer.phone,
                        createdAt = existingCustomer.createdAt
                    }
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new Result<Customer>("Error updating customer: " + ex.Message));
            }
            }
        }
}
