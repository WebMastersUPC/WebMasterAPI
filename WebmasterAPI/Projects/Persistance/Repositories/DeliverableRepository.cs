using Microsoft.EntityFrameworkCore;
using WebmasterAPI.Authentication.Domain.Models;
using WebmasterAPI.Authentication.Domain.Repositories;
using WebmasterAPI.Projects.Domain.Models;
using WebmasterAPI.Projects.Domain.Repositories;
using WebmasterAPI.Shared.Persistence.Contexts;
using WebmasterAPI.Shared.Persistence.Repositories;

namespace WebmasterAPI.Projects.Domain.Persistance.Repositories;

public class DeliverableRepository:BaseRepository,IDeliverableRepository  {

    public DeliverableRepository(AppDbContext context) : base(context)
    {
        
    }

    public async Task AddSync(Deliverable deliverable)
    {
        await _Context.Deliverables.AddAsync(deliverable);
    }

    public async Task<Deliverable> FindDeliverableIdAsync(long id)
    {
        return await _Context.Deliverables.Include(d => d.Developer).Include(d => d.Project)
            .FirstOrDefaultAsync(d => d.deliverable_id == id);
    }

    public async Task<List<Deliverable>> ListAsync()
    {
        return await _Context.Deliverables.Include(d => d.Developer).Include(d => d.Project).ToListAsync();
    }

    public void Remove(Deliverable deliverable)
    {
        _Context.Deliverables.Remove(deliverable);
    }
    
    public async Task UpdateAsync(Deliverable deliverable)
    {
        _Context.Deliverables.Update(deliverable);
        await _Context.SaveChangesAsync();
    }
    
    public Task<bool> ExistByIdAsync(long id)
    {
        return _Context.Deliverables.AnyAsync(d => d.deliverable_id == id);
    }

}