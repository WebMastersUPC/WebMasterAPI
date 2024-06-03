using WebmasterAPI.Projects.Domain.Services.Communication;

namespace WebmasterAPI.Projects.Domain.Services;

public interface IDeliverableService {

    Task<List<DeliverableResponse>> ListDeliverablesAsync();
    
    Task<DeliverableResponse> GetDeliverableByIdAsync(long id);

    Task<DeliverableResponse> DeleteDeliverableByIdAsync(long id);
    
    Task UpdateDeliverableAsync(long id, DeliverableRequest updateRequest);
    
    
}