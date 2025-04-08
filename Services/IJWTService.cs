public interface IJwtService
{
    string GenerateToken(Guid userId, string role, Guid storeId);
}
