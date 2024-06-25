using WebmasterAPI.ProjectManagement.Domain.Models;
using WebmasterAPI.ProjectManagement.Domain.Services.Communication;

namespace WebmasterAPI.ProjectManagement.Domain.Services;

public interface IDeliverableService {
    
    Task<List<DeliverableResponse>> GetDeliverableByProjectIdAsync(long projectId);
    
    Task<DeliverableResponse>DeleteDeliverableByProjectIdandDeliverableIdAsync(long projectId, int orderNumber);
    
    Task UpdateDeliverableByProjectIdandDeliverableIdAsync(long projectId, long deliverableId, DeliverableUpdateRequest updateRequest);
    
    Task ApproveOrRejectDeliverableAsync(int orderNumber, string newState);
    
    Task AddDeliverableToProjectAsync(long projectId, CreateDeliverableByProjectIdRequest request);

    Task<UploadDeliverableResponse> UploadDeliverableAsync(long projectId, int deliverableId, long developerId, UploadDeliverableRequest upload);
    
    Task <UploadDeliverableResponse>GetUploadedDeliverableByProjectIdAndDeliverableIdAsync(long projectId, int orderNumber);
    
    Task<DeliverableResponse> GetDeliverableByProjectIdAndDeliverableIdAsync(long projectId, long deliverableId);
}