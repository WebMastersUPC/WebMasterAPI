using WebmasterAPI.ProjectManagement.Domain.Models;
using WebmasterAPI.ProjectManagement.Domain.Services.Communication;

namespace WebmasterAPI.ProjectManagement.Domain.Repositories;

public interface IDeliverableRepository
{

    Task AddSync(Deliverable deliverable);
    
    Task UpdateAsync(Deliverable deliverable);
    
    Task UpdateDeliverableByProjectIdandDeliverableIdAsync(long projectId, long deliverableId, Deliverable deliverable);
    
    Task<Deliverable> FindDeliverableIdAsync(long id);

    Task <List<Deliverable>> ListAsync();
    
    Task<Deliverable> GetLastDeliverableByDeveloperIdAsync(long developerId);
    
    Task <UploadDeliverableResponse> uploadDeliverableAsync(long deliverableId, long developerId, UploadDeliverableResponse upload);
    
    //Task <List<Deliverable>> ListByProjectIdAsync(long projectId);
    Task <List<DeliverableResponse>> ListByProjectIdAsync(long projectId);

    Task RemoveByIdAsync(long id);
    
    Task RemoveDeliverableByProjectIdandDeliverableIdAsync(long projectId, long deliverableId);
    
    Task<bool> ExistByIdAsync(long id);
    Task<bool> ProjectExistsAsync(long projectId);
    Task<bool> DeveloperExistsAsync(long developerId);
    //Task<bool> DeveloperBelongsToProjectAsync(long projectId, long developerId);
    
}