using WebmasterAPI.Models;

namespace WebmasterAPI.UserManagement.Authorization.Handlers.Interface;

public interface IJwtHandler
{
    public string GenerateToken(User user);
    public int? ValidateToken(string token);
}   