namespace ServiceCore.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string userName);
    }
}
