using WebmasterAPI.Authentication.Domain.Services.Communication;
using WebmasterAPI.Authentication.Resources;
using WebmasterAPI.Models;

namespace WebmasterAPI.Authentication.Domain.Services;

public interface IUserService
{
    Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model);
    Task RegisterDeveloperAsync(DeveloperResource model);
    Task RegisterEnterpriseAsync(RegisterEnterpriseRequest model);
}