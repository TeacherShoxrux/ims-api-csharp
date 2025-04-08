namespace imsapi.Services;
public interface IJwtService
{
    string GenerateToken(int userId, string role, int storeId);
}
