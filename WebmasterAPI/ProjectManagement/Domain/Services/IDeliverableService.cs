using WebmasterAPI.ProjectManagement.Domain.Models;
using WebmasterAPI.ProjectManagement.Domain.Services.Communication;

namespace WebmasterAPI.ProjectManagement.Domain.Services;

public interface IDeliverableService {
    
    Task<List<DeliverableResponse>> GetDeliverableByProjectIdAsync(long projectId);

    Task<DeliverableResponse> GetDeliverableByProjectIdAndDeliverableIdAsync(long projectId, long deliverableId);
    
    Task<DeliverableResponse>DeleteDeliverableByProjectIdandDeliverableIdAsync(long projectId, int orderNumber);
    
    Task UpdateDeliverableByProjectIdandDeliverableIdAsync(long projectId, long deliverableId, DeliverableUpdateRequest updateRequest);
    
    Task ApproveOrRejectDeliverableAsync(long deliverableId, string newState);
    
    Task AddDeliverableToProjectAsync(long projectId, CreateDeliverableByProjectIdRequest request);

    Task<UploadDeliverableResponse> UploadDeliverableAsync(long projectId, long deliverableId,UploadDeliverableRequest upload);
    
    Task<UploadDeliverableResponse> GetUploadedDeliverableByProjectIdAndDeliverableIdAsync(long projectId, long deliverableId);
}