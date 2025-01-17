using Microsoft.EntityFrameworkCore;
using WebmasterAPI.Authentication.Domain.Repositories;
using WebmasterAPI.Models;
using WebmasterAPI.Shared.Persistence.Contexts;
using WebmasterAPI.Shared.Persistence.Repositories;

namespace WebmasterAPI.Authentication.Persistence.Repositories;

public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
    
    public async Task AddAsync(User user)
    {
        await _Context.Users.AddAsync(user);
    }

    public async Task<User> FindByIdAsync(int id)
    {
        return await _Context.Users.FindAsync(id);
    }

    public async Task<User> FindByEmailAsync(string email)
    {
        return await _Context.Users.SingleOrDefaultAsync(user => user.mail == email);
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _Context.Users.AnyAsync(user => user.mail == email);
    }

    public User FindById(int id)
    {
        return _Context.Users.Find(id);
    }

    public void Update(User user)
    {
        _Context.Users.Update(user);
    }

    public void Remove(User user)
    {
        _Context.Users.Remove(user);
    }
}