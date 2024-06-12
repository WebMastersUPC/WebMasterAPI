using Microsoft.EntityFrameworkCore;
using WebmasterAPI.Authentication.Domain.Models;
using WebmasterAPI.Authentication.Domain.Repositories;
using WebmasterAPI.ProjectManagement.Domain.Models;
using WebmasterAPI.ProjectManagement.Domain.Repositories;
using WebmasterAPI.ProjectManagement.Domain.Services.Communication;
using WebmasterAPI.Shared.Persistence.Contexts;
using WebmasterAPI.Shared.Persistence.Repositories;

namespace WebmasterAPI.ProjectManagement.Domain.Persistance.Repositories;

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
        var deliverables = await _Context.Deliverables.Where(d => d.projectID == projectId).ToListAsync();
        _Context.Deliverables.RemoveRange(deliverables);
        await _Context.SaveChangesAsync();
    }
    
    public async Task RemoveDeliverableByProjectIdandDeliverableIdAsync(long projectId, long deliverableId)
    {
        var deliverable = await _Context.Deliverables.FirstOrDefaultAsync(d => d.projectID == projectId && d.deliverable_id == deliverableId);
        if (deliverable == null)
        {
            throw new Exception($"Deliverable with id {deliverableId} not found in project with id {projectId}");
        }

        _Context.Deliverables.Remove(deliverable);
        await _Context.SaveChangesAsync();
    }


    public async Task<UploadDeliverableResponse> uploadDeliverableAsync(long deliverableId, long developerId, UploadDeliverableResponse upload)
    {
        // Obtén el Deliverable de la base de datos
        var deliverable = await _Context.Deliverables.FindAsync(deliverableId);

        // Actualiza la descripción, el archivo y el estado del Deliverable
        deliverable.developerDescription = upload.developerDescription;
        deliverable.file = upload.file;
        deliverable.state = "En espera de revisión";
        deliverable.createdAt = DateTime.UtcNow;

        // Guarda los cambios en la base de datos
        _Context.Deliverables.Update(deliverable);
        await _Context.SaveChangesAsync();

        // Devuelve el Deliverable actualizado
        return new UploadDeliverableResponse { developerDescription = deliverable.developerDescription, file = deliverable.file };
    }

    public async Task<List<DeliverableResponse>> ListByProjectIdAsync(long projectId)
    {
        var deliverables = await _Context.Deliverables
            .Where(d => d.projectID == projectId).ToListAsync();

        var deliverableResponses = new List<DeliverableResponse>();

        foreach (var deliverable in deliverables)
        {
            _Context.Entry(deliverable).Reference(d => d.Developer).Load();
            _Context.Entry(deliverable).Reference(d => d.Project).Load();

            var deliverableResponse = new DeliverableResponse
            {
                deliverable_id = deliverable.deliverable_id,
                title = deliverable.title,
                description = deliverable.description,
                state = deliverable.state,
                deadline = deliverable.deadline,
                projectID = deliverable.projectID,
                nameProject = deliverable.Project.nameProject, // Asegúrate de que 'name' es la propiedad correcta en tu entidad Project
                developer_id = deliverable.developer_id,
                firstName = deliverable.Developer.firstName // Asegúrate de que 'firstName' es la propiedad correcta en tu entidad Developer
            };

            deliverableResponses.Add(deliverableResponse);
        }

        return deliverableResponses;
    }
    
    /*public async Task<List<Deliverable>> ListByProjectIdAsync(long projectId)
    {
        return await _Context.Deliverables.Include(d=>d.Developer).Include(d=>d.Project)
            .Where(d => d.projectID == projectId).ToListAsync();
    }*/

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
    
    public async Task UpdateDeliverableByProjectIdandDeliverableIdAsync(long projectId, long deliverableId, Deliverable deliverable)
    {
        var deliverableToUpdate = await _Context.Deliverables.FirstOrDefaultAsync(d => d.projectID == projectId && d.deliverable_id == deliverableId);
        if (deliverableToUpdate == null)
        {
            throw new Exception($"Deliverable with id {deliverableId} not found in project with id {projectId}");
        }

        deliverableToUpdate.title = deliverable.title;
        deliverableToUpdate.description = deliverable.description;
        deliverableToUpdate.state = deliverable.state;
        deliverableToUpdate.developer_id = deliverable.developer_id;
        deliverableToUpdate.projectID = deliverable.projectID;
        await _Context.SaveChangesAsync();
    }
    
    public Task<bool> ExistByIdAsync(long id)
    {
        return _Context.Deliverables.AnyAsync(d => d.deliverable_id == id);
    }
    
    public async Task<bool> ProjectExistsAsync(long projectId)
    {
        return await _Context.Projects.AnyAsync(p => p.projectID == projectId);
    }

    public async Task<bool> DeveloperExistsAsync(long developerId)
    {
        return await _Context.Developers.AnyAsync(d => d.developer_id == developerId);
    }
    
    /*public async Task<bool> DeveloperBelongsToProjectAsync(long projectId, long developerId)
    {
        return await _Context.Projects.AnyAsync(p => p.project_id == projectId && p.Developers.Any(d => d.developer_id == developerId));
    }*/
    
    
    public async Task<Deliverable> GetLastDeliverableByDeveloperIdAsync(long developerId)
    {
        // Obtén todos los Deliverables del desarrollador especificado
        var deliverables = await _Context.Deliverables
            .Where(d => d.developer_id == developerId)
            .ToListAsync();

        // Si no hay Deliverables, devuelve null
        if (!deliverables.Any())
        {
            return null;
        }

        // Ordena los Deliverables por su fecha de creación y selecciona el más reciente
        var lastDeliverable = deliverables
            .OrderByDescending(d => d.createdAt)
            .First();

        return lastDeliverable;
    }
    
}