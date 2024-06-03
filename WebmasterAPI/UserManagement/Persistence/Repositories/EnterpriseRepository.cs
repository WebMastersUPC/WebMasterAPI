using Microsoft.EntityFrameworkCore;
using WebmasterAPI.Authentication.Domain.Models;
using WebmasterAPI.Authentication.Domain.Repositories;
using WebmasterAPI.Authentication.Domain.Services.Communication;
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

    public async Task<Enterprise> FindByIdAsync(long id)
    {
        return await _Context.Enterprises.Include(e => e.User).FirstOrDefaultAsync(e => e.user_id == id);
    }

    public void Remove(Enterprise enterprise)
    {
        _Context.Enterprises.Remove(enterprise);
    }

    public async Task UpdateAsync(Enterprise enterprise)
    {
        _Context.Enterprises.Update(enterprise);
        await _Context.SaveChangesAsync();
    }
}