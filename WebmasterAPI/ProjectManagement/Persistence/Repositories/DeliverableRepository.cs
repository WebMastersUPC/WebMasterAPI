using Microsoft.EntityFrameworkCore;
using WebmasterAPI.ProjectManagement.Domain.Models;
using WebmasterAPI.ProjectManagement.Domain.Repositories;
using WebmasterAPI.ProjectManagement.Domain.Services.Communication;
using WebmasterAPI.Shared.Persistence.Contexts;
using WebmasterAPI.Shared.Persistence.Repositories;

namespace WebmasterAPI.ProjectManagement.Domain.Persistance.Repositories;

public class DeliverableRepository : BaseRepository, IDeliverableRepository  
{
    public DeliverableRepository(AppDbContext context) : base(context)
    {
    }

    public async Task AddSync(Deliverable deliverable)
    {
        await _Context.Deliverables.AddAsync(deliverable);
    }

    public async Task UpdateAsync(Deliverable deliverable)
    {
        _Context.Deliverables.Update(deliverable);
        await _Context.SaveChangesAsync();
    }

    public async Task UpdateDeliverableByProjectIdandDeliverableIdAsync(long projectId, int orderNumber, Deliverable deliverable)
    {
        var deliverableToUpdate = await _Context.Deliverables.FirstOrDefaultAsync(d => d.projectID == projectId && d.orderNumber == orderNumber);
        if (deliverableToUpdate == null)
        {
            throw new Exception($"Deliverable with order number {orderNumber} not found in project with id {projectId}");
        }

        deliverableToUpdate.title = deliverable.title;
        deliverableToUpdate.description = deliverable.description;
        deliverable.deadlineDateValue = deliverable.deadlineDateValue;
        
        if (TimeSpan.TryParse(deliverable.deadlineTime, out TimeSpan deadlineTime))
        {
            // convierte el TimeSpan a string antes de asignarlo a deliverableToUpdate.deadlineTime
            deliverableToUpdate.deadlineTime = deadlineTime.ToString();
        }
        else
        {
            throw new Exception("Invalid deadlineTime format. It should be in the format HH:MM:SS.");
        }

        await _Context.SaveChangesAsync();
    }

    public async Task<Deliverable> FindDeliverableByProjectIdAndOrderNumberAsync(long projectId, int orderNumber)
    {
        return await _Context.Deliverables
            .FirstOrDefaultAsync(d => d.projectID == projectId && d.orderNumber == orderNumber);
    }
    public async Task<Deliverable> FindDeliverableByorderNumberAsync(int orderNumber)
    {
        return await _Context.Deliverables
            .FirstOrDefaultAsync(d => d.orderNumber == orderNumber);
    }
    
    public async Task<Deliverable> FindDeliverableByIdAsync(long deliverableId)
    {
        return await _Context.Deliverables
            .FirstOrDefaultAsync(d => d.deliverable_id == deliverableId);
    }

    public async Task<List<DeliverableResponse>> ListByProjectIdAsync(long projectId)
    {
        var deliverables = await _Context.Deliverables
            .Where(d => d.projectID == projectId)
            .Include(d => d.Developer)
            .Include(d => d.Project)
            .ToListAsync();

        return deliverables.Select(d => new DeliverableResponse
        {
            deliverable_id = d.deliverable_id,
            title = d.title,
            description = d.description,
            developerDescription = d.developerDescription,
            state = d.state,
            file=d.file,
            deadlineDateValue = d.deadlineDateValue,
            // Trata deadlineTime como string
            deadlineTime = d.deadlineTime,
            orderNumber = d.orderNumber,
            projectID = d.projectID,
            nameProject = d.Project.nameProject,
            developer_id = d.developer_id,
            firstName = d.Developer.firstName
        }).ToList();
    }

    public async Task RemoveDeliverableByProjectIdandDeliverableIdAsync(long projectId, int orderNumber)
    {
        var deliverable = await _Context.Deliverables.FirstOrDefaultAsync(d => d.projectID == projectId && d.orderNumber == orderNumber);
        if (deliverable == null)
        {
            throw new Exception($"Entregable con el número de orden {orderNumber} no ha sido encontrado en el proyecto con ID {projectId}");
        }

        _Context.Deliverables.Remove(deliverable);
        await _Context.SaveChangesAsync();
    }

    public async Task<bool> ProjectExistsAsync(long projectId)
    {
        return await _Context.Projects.AnyAsync(p => p.projectID == projectId);
    }

    public async Task<Deliverable> GetHighestOrderNumberByProjectIdAsync(long projectId)
    {
        return await _Context.Deliverables
            .Where(d => d.projectID == projectId)
            .OrderByDescending(d => d.orderNumber)
            .FirstOrDefaultAsync();
    }

    public async Task<Deliverable> GetLastUploadedDeliverableByDeveloperIdAndProjectId(long projectId)
    {
        return await _Context.Deliverables
            .Where(d=> d.projectID == projectId && d.state != "En espera de entrega")
            .OrderByDescending(d => d.orderNumber)
            .FirstOrDefaultAsync();
    }

    public async Task<Deliverable> GetUploadedDeliverableByProjectIdAndDeliverableIdAsync(long projectId, long deliverableId)
    {
        return await _Context.Deliverables
            .FirstOrDefaultAsync(d => d.projectID == projectId && d.deliverable_id == deliverableId);
    }
    
    public async Task<Deliverable> GetDeliverableByProjectIdAndDeliverableIdAsync(long projectId, long deliverableId)
    {
        return await _Context.Deliverables
            .FirstOrDefaultAsync(d => d.projectID == projectId && d.deliverable_id == deliverableId);
    }
    
    
}
