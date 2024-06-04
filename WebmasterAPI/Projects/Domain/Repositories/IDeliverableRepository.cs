using WebmasterAPI.Projects.Domain.Models;

namespace WebmasterAPI.Projects.Domain.Repositories;

public interface IDeliverableRepository
{

    Task AddSync(Deliverable deliverable);
    
    Task UpdateAsync(Deliverable deliverable);
    
    Task<Deliverable> FindDeliverableIdAsync(long id);

    Task <List<Deliverable>> ListAsync();

    Task RemoveByIdAsync(long id);
    
    Task<bool> ExistByIdAsync(long id);
    Task<bool> ProjectExistsAsync(long projectId);
    Task<bool> DeveloperExistsAsync(long developerId);
    //Task<bool> DeveloperBelongsToProjectAsync(long projectId, long developerId);
    
}