namespace WebmasterAPI.UserManagement.Authorization.Handlers.Interface;

public interface IJwtHandler
{
    string GenerateToken(int email);
}