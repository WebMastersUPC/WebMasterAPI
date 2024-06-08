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
    
    public async Task RemoveByProjectIdAsync(long projectId)
    {
        var deliverables = await _Context.Deliverables.Where(d => d.project_id == projectId).ToListAsync();
        _Context.Deliverables.RemoveRange(deliverables);
        await _Context.SaveChangesAsync();
    }
    
    public async Task RemoveDeliverableByProjectIdandDeliverableIdAsync(long projectId, long deliverableId)
    {
        var deliverable = await _Context.Deliverables.FirstOrDefaultAsync(d => d.project_id == projectId && d.deliverable_id == deliverableId);
        if (deliverable == null)
        {
            throw new Exception($"Deliverable with id {deliverableId} not found in project with id {projectId}");
        }

        _Context.Deliverables.Remove(deliverable);
        await _Context.SaveChangesAsync();
    }
    
    public async Task<List<Deliverable>> ListByProjectIdAsync(long projectId)
    {
        return await _Context.Deliverables.Include(d => d.Project)
            .Where(d => d.project_id == projectId).ToListAsync();
    }

    public async Task<Deliverable> FindDeliverableIdAsync(long id)
    {
        return await _Context.Deliverables.Include(d => d.Project)
            .FirstOrDefaultAsync(d => d.deliverable_id == id);
    }

    //Revisar porque agarra todos los deliverables se me filtra la base de datos puñeta
    public async Task<List<Deliverable>> ListAsync()
    {
        return await _Context.Deliverables.Include(d => d.Developer).Include(d => d.Project).ToListAsync();
    }




    public async Task RemoveByIdAsync(long id)
    {
        var deliverable = await _Context.Deliverables.FindAsync(id);
        if (deliverable == null)
        {
            throw new Exception($"Deliverable with id {id} not found");
        }

        _Context.Deliverables.Remove(deliverable);
        await _Context.SaveChangesAsync();
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
    
    public async Task<bool> ProjectExistsAsync(long projectId)
    {
        return await _Context.Projects.AnyAsync(p => p.project_id == projectId);
    }

    public async Task<bool> DeveloperExistsAsync(long developerId)
    {
        return await _Context.Developers.AnyAsync(d => d.developer_id == developerId);
    }
    
    /*public async Task<bool> DeveloperBelongsToProjectAsync(long projectId, long developerId)
    {
        return await _Context.Projects.AnyAsync(p => p.project_id == projectId && p.Developers.Any(d => d.developer_id == developerId));
    }*/
}