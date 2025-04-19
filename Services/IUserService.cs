using DTO.User;
using imsapi.DTO;
using IMSAPI.DTO.User;

namespace imsapi.Services
{
    public interface IUserService
    {
        // Define methods for the user service
        Task<Result<List<User>>> GetAllUsersByStoreIdAsync(int storeId);
        Task<Result<User?>> GetUserByIdAsync(int userId);
        Task<Result<User>> CreateUserAsync(int storeId,NewUser user);
        Task<Result<User>> UpdateUserAsync(int userId, UpdateUser user);
        Task<Result<User>> DeleteUserAsync(int storeId,int userId);
        Task<Result<Session>> Authenticate(UserLogin login);
    }


}