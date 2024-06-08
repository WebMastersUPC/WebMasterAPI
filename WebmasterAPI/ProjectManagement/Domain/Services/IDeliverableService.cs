using WebmasterAPI.ProjectManagement.Domain.Services.Communication;

namespace WebmasterAPI.ProjectManagement.Domain.Services;

public interface IDeliverableService {

    Task<List<DeliverableResponse>> ListDeliverablesAsync();
    Task<List<DeliverableResponse>> GetDeliverableByProjectIdAsync(long projectId);
    
    Task<DeliverableResponse> GetDeliverableByIdAsync(long id);

    Task<DeliverableResponse> DeleteDeliverableByIdAsync(long id);
    
    Task<DeliverableResponse>DeleteDeliverableByProjectIdandDeliverableIdAsync(long projectId, long deliverableId);
    
    Task UpdateDeliverableAsync(long id, DeliverableUpdateRequest updateRequest);
    
    Task UpdateDeliverableByProjectIdandDeliverableIdAsync(long projectId, long deliverableId, DeliverableUpdateRequest updateRequest);
    
    Task AddDeliverableAsync(CreateDeliverableRequest deliverable);
    
    Task AddDeliverableToProjectAsync(long projectId, CreateDeliverableByProjectIdRequest request);

    



}