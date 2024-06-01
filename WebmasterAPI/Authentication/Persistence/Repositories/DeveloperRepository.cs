using WebmasterAPI.Authentication.Domain.Models;
using WebmasterAPI.Authentication.Domain.Repositories;
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

    public async Task<Developer> FindByIdAsync(int id)
    {
        return await _Context.Developers.FindAsync(id);
    }

    public void Remove(Developer developer)
    {
        _Context.Developers.Remove(developer);
    }
}