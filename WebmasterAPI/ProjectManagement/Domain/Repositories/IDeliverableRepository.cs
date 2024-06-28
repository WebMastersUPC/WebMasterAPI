using WebmasterAPI.ProjectManagement.Domain.Models;
using WebmasterAPI.ProjectManagement.Domain.Services.Communication;

namespace WebmasterAPI.ProjectManagement.Domain.Repositories;

public interface IDeliverableRepository
{

    Task AddSync(Deliverable deliverable);
    
    Task UpdateAsync(Deliverable deliverable);
    
    Task UpdateDeliverableByProjectIdandDeliverableIdAsync(long projectId, int orderNumber, Deliverable deliverable);
    
    Task <Deliverable> FindDeliverableByProjectIdAndOrderNumberAsync(long projectId, int orderNumber);
    Task<Deliverable> FindDeliverableByIdAsync(long deliverableId);
    
    Task<Deliverable> FindDeliverableByorderNumberAsync(int orderNumber);
    Task <List<DeliverableResponse>> ListByProjectIdAsync(long projectId);
    
    Task RemoveDeliverableByProjectIdandDeliverableIdAsync(long projectId, int orderNumber);
    
    Task<bool> ProjectExistsAsync(long projectId);
    
    Task <Deliverable> GetHighestOrderNumberByProjectIdAsync(long projectId);
    
    Task<Deliverable> GetLastUploadedDeliverableByDeveloperIdAndProjectId(long projectId);
    
    Task<Deliverable> GetDeliverableByProjectIdAndDeliverableIdAsync(long projectId, long deliverableId);
    
}