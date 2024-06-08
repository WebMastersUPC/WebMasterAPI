using WebmasterAPI.Projects.Domain.Services.Communication;

namespace WebmasterAPI.Projects.Domain.Services;

public interface IDeliverableService {

    Task<List<DeliverableResponse>> ListDeliverablesAsync();
    Task<List<DeliverableResponse>> GetDeliverableByProjectIdAsync(long projectId);
    
    Task<DeliverableResponse> GetDeliverableByIdAsync(long id);

    Task<DeliverableResponse> DeleteDeliverableByIdAsync(long id);
    
    Task<DeliverableResponse>DeleteDeliverableByProjectIdandDeliverableIdAsync(long projectId, long deliverableId);
    
    Task UpdateDeliverableAsync(long id, DeliverableUpdateRequest updateRequest);
    
    Task AddDeliverableAsync(CreateDeliverableRequest deliverable);
    
    Task AddDeliverableToProjectAsync(long projectId, CreateDeliverableByProjectIdRequest request);

    



}