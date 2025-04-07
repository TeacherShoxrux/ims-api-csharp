using imsapi.Data;
using imsapi.DTO;
using imsapi.DTO.Store;
using imsapi.Utils;
using IMSAPI.DTO.Store;
using Microsoft.EntityFrameworkCore;

namespace imsapi.Services;

public class StoreService : IStoreService
{
    public StoreService(AppDbContext context)
    {
        _context = context;
    }
    private readonly AppDbContext _context;
    public async Task<Result<Store>> CreateStoreAsync(NewStore store)
    {
        try
        {
            // Check if the store already exists
            
            

           var newStore= _context.Stores.Add(new()
            { 
                phone = store.phone,
                address = store.address,
                name = store.storeName,
            });
            _context.SaveChanges();
            var director=_context.Users.Add(new()
            {
                fullName= store.storeName,
                storeId = newStore.Entity.id,
                passwordHash = store.password.Sha256(),
                phone = store.phone,
                role = Data.Entities.ERole.Director,
            });
            await _context.SaveChangesAsync();

            return new (true)
            {
                Data= new(){
                    id = newStore.Entity.id,
                    name = newStore.Entity.name,
                    address = newStore.Entity.address,
                    phone = newStore.Entity.phone
                }
            };

            // return Task.FromResult(Result<Store>.Success(newStore));
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }

    public Task<Result<Store>> DeleteStoreAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<Store>>> GetAllStoresAsync()
    {
        try
        {
            var stores = _context.Stores.ToList();
            if (stores == null || stores.Count == 0)
            {
                return Task.FromResult(new Result<List<Store>>(false));
            }
            return Task.FromResult(new Result<List<Store>>(true)
            {
                Data = stores.Select(x => new Store()
                {
                    id = x.id,
                    name = x.name,
                    address = x.address,
                    phone = x.phone
                }).ToList()
            });
        }
        catch (System.Exception ex)
        {
            // Log the exception
            Console.WriteLine(ex.Message);
            // Handle the exception as needed
            return Task.FromResult(new Result<List<Store>>(false,"An error occurred while retrieving the stores."));
        }
        
    }

    public Task<Result<Store?>> GetStoreByIdAsync(int id)
    {
        try
        {
            var store = _context.Stores.FirstOrDefault(x => x.id == id);
            if (store == null)
            {
                return Task.FromResult(new Result<Store?>(false));
            }
            return Task.FromResult(new Result<Store?>(true)
            {
                Data = new()
                {
                    id = store.id,
                    name = store.name,
                    address = store.address,
                    phone = store.phone
                }
            });
        }
        catch (Exception ex)
        {
            return Task.FromResult(new Result<Store?>(false,"An error occurred while retrieving the store."));
        }
    }

    public Task<Result<Store>> UpdateStoreAsync(int id, NewStore store)
    {
        try
        {
            var storeToUpdate = _context.Stores.FirstOrDefault(x => x.id == id);
            if (storeToUpdate == null)
            {
                return Task.FromResult(new Result<Store>(false));
            }
            storeToUpdate.name = store.storeName;
            storeToUpdate.phone = store.phone;
            storeToUpdate.address = store.address;
            _context.SaveChanges();
            return Task.FromResult( new Result<Store>(true)
            {
                Data = new()
                {
                    id = storeToUpdate.id,
                    name = storeToUpdate.name,
                    address = storeToUpdate.address,
                    phone = storeToUpdate.phone
                }
            });
        }
        catch (System.Exception ex)
        {
            // Log the exception
            Console.WriteLine(ex.Message);
            // Handle the exception as needed
            return Task.FromResult(new Result<Store>(false,"An error occurred while updating the store."));
        }
        
       
    }
}