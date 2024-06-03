using Microsoft.EntityFrameworkCore;
using WebmasterAPI.Authentication.Domain.Models;
using WebmasterAPI.Authentication.Domain.Repositories;
using WebmasterAPI.Authentication.Domain.Services.Communication;
using WebmasterAPI.Shared.Persistence.Contexts;
using WebmasterAPI.Shared.Persistence.Repositories;

namespace WebmasterAPI.Authentication.Persistence.Repositories;

public class DeveloperRepository : BaseRepository, IDeveloperRepository
{
    public DeveloperRepository(AppDbContext context) : base(context)
    {
    }

    public async Task AddAsync(Developer developer)
    {
        await _Context.Developers.AddAsync(developer);
    }

    public async Task<Developer> FindByIdAsync(long id)
    {
        return await _Context.Developers.Include(d => d.User).FirstOrDefaultAsync(d => d.user_id == id);
    }

    public void Remove(Developer developer)
    {
        _Context.Developers.Remove(developer);
    }
    
    public async Task<List<Developer>> ListAsync()
    {
        return await _Context.Developers.Include(d => d.User).ToListAsync();
    }
    
    public async Task UpdateAsync(Developer developer)
    {
        _Context.Developers.Update(developer);
        await _Context.SaveChangesAsync();
    }
}