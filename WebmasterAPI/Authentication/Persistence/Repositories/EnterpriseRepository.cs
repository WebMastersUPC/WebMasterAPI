using WebmasterAPI.Authentication.Domain.Models;
using WebmasterAPI.Authentication.Domain.Repositories;
using WebmasterAPI.Shared.Persistence.Contexts;
using WebmasterAPI.Shared.Persistence.Repositories;

namespace WebmasterAPI.Authentication.Persistence.Repositories;

public class EnterpriseRepository : BaseRepository, IEnterpriseRepository
{
    public EnterpriseRepository(AppDbContext context) : base(context)
    {
    }

    public async Task AddAsync(Enterprise enterprise)
    {
        await _Context.Enterprises.AddAsync(enterprise);
    }

    public async Task<Enterprise> FindByIdAsync(int id)
    {
        return await _Context.Enterprises.FindAsync(id);
    }

    public void Remove(Enterprise enterprise)
    {
        _Context.Enterprises.Remove(enterprise);
    }
}