using WebmasterAPI.Authentication.Domain.Services.Communication;
using WebmasterAPI.Models;

namespace WebmasterAPI.Authentication.Domain.Services;

public interface IUserService
{
    Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model);
    Task<IEnumerable<User>> ListAsync();
    Task<User> GetByIdAsync(int id);
    Task RegisterAsync(RegisterRequest model);
    //Task UpdateAsync(int id, UpdateRequest model);
    // Task DeleteAsync(int id);
}