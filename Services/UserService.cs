using DTO.User;
using imsapi.Data;
using imsapi.DTO;
using imsapi.Utils;
using IMSAPI.DTO.User;

namespace imsapi.Services
{
    public class UserService : IUserService
    {
        public UserService(AppDbContext dbContext,IJwtService jwtService)
        {
            _dbContext = dbContext;
            _jwtService=jwtService;
        }
        private readonly AppDbContext _dbContext;
        private readonly IJwtService _jwtService;

        public Task<Result<User>> CreateUserAsync(int storeId, NewUser user)
        {
            try
            {
                var existingUser = _dbContext.Users.FirstOrDefault(u => u.phone == user.phone && u.storeId == storeId);
                if (existingUser != null)
                {
                    return Task.FromResult(new Result<User>("User already exists"));
                }
                var newUser = _dbContext.Users.Add(new()
                {
                    fullName = user.fullName,
                    storeId = storeId,
                    passwordHash = user.password.Sha256(),
                    phone = user.phone,
                });
                _dbContext.SaveChanges();
                return Task.FromResult(new Result<User>(true)
                {
                    Data = new()
                    {
                        id = newUser.Entity.id,
                        fullName = newUser.Entity.fullName,
                        phone = newUser.Entity.phone,
                        role = Enum.GetName(newUser.Entity.role),
                    }
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new Result<User>(ex.Message));
            }
        }

        public Task<Result<User>> DeleteUserAsync(int storeId, int userId)
        {
            try
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.id == userId && u.storeId == storeId);
                if (user == null)
                {
                    return Task.FromResult(new Result<User>("User not found"));
                }
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
                return Task.FromResult(new Result<User>(true)
                {
                    Data = new()
                    {
                        id = user.id,
                        fullName = user.fullName,
                        phone = user.phone,
                        role = Enum.GetName(user.role),
                    }
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new Result<User>(ex.Message));
            }
        }

        public Task<Result<List<User>>> GetAllUsersByStoreIdAsync(int storeId)
        {
            try
            {
                var users = _dbContext.Users.Where(u => u.storeId == storeId).ToList();
                if (users == null || users.Count == 0)
                {
                    return Task.FromResult(new Result<List<User>>(false));
                }
                return Task.FromResult(new Result<List<User>>(true)
                {
                    Data = users.Select(x => new User()
                    {
                        id = x.id,
                        fullName = x.fullName,
                        phone = x.phone,
                        role = Enum.GetName(x.role),
                        email=x.email
                        
                    }).ToList()
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new Result<List<User>>(ex.Message));
            }
        }

        public Task<Result<User?>> GetUserByIdAsync(int userId)
        {
            try
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.id == userId);
                if (user == null)
                {
                    return Task.FromResult(new Result<User?>("User not found"));
                }
                return Task.FromResult(new Result<User?>(true)
                {
                    Data = new()
                    {
                        id = user.id,
                        fullName = user.fullName,
                        phone = user.phone,
                        role = Enum.GetName(user.role),
                    }
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new Result<User?>(ex.Message));
            }
        }

        public Task<Result<User>> UpdateUserAsync(int userId, UpdateUser user)
        {
            try
            {
                var existingUser = _dbContext.Users.FirstOrDefault(u => u.id == userId);
                if (existingUser == null)
                {
                    return Task.FromResult(new Result<User>("User not found"));
                }
                existingUser.email = user.email;
                existingUser.image = user.image;
                _dbContext.SaveChanges();
                return Task.FromResult(new Result<User>(true)
                {
                    Data = new()
                    {
                        id = existingUser.id,
                        fullName = existingUser.fullName,
                        phone = existingUser.phone,
                        role = Enum.GetName(existingUser.role),
                        image=existingUser.image,
                        
                    }
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new Result<User>(ex.Message));
            }
        }

        public Task<Result<Session>> Authenticate(UserLogin login)
        {
            try
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.phone == login.phone && u.passwordHash == login.password.Sha256());
                if (user == null)
                {
                    return Task.FromResult(new Result<Session>("Invalid credentials"));
                }
                return Task.FromResult(new Result<Session>(true)
                {
                    Data = new()
                    {
                        
                        UserId = user.id,
                        CreatedAt = DateTime.UtcNow,
                        ExpiresAt = DateTime.UtcNow.AddHours(1440),
                        accessToken = _jwtService.GenerateToken(user.id, Enum.GetName(user.role).ToString(),user.storeId),
                        refreshoken = Guid.NewGuid().ToString(),
                    }
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new Result<Session>(ex.Message));
            }
        }
    }
}