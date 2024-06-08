using WebmasterAPI.Authentication.Domain.Models;
using WebmasterAPI.Authentication.Domain.Services.Communication;
using WebmasterAPI.Authentication.Resources;
using WebmasterAPI.Models;

namespace WebmasterAPI.Authentication.Domain.Services;

public interface IUserService
{
    Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model);
    Task RegisterDeveloperAsync(RegisterDeveloperRequest model);
    Task RegisterEnterpriseAsync(RegisterEnterpriseRequest model);
    Task<User> GetByIdAsync(int id);
    
}