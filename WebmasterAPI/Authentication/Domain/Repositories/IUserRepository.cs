using WebmasterAPI.Models;

namespace WebmasterAPI.Authentication.Domain.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User> FindByIdAsync(int id);
    Task<User> FindByEmailAsync(string email);
    Task<bool> ExistsByEmailAsync(string email);
    User FindById(int id);
    void Update(User user);
    void Remove(User user);
}