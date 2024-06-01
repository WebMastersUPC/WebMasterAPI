using WebmasterAPI.Shared.Domain.Repositories;
using WebmasterAPI.Shared.Persistence.Contexts;

namespace WebmasterAPI.Shared.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    protected readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}
