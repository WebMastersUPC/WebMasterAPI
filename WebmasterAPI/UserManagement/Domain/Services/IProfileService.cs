using WebmasterAPI.Authentication.Domain.Models;
using WebmasterAPI.Authentication.Domain.Services.Communication;

namespace WebmasterAPI.Authentication.Domain.Services;

public interface IProfileService
{
    Task<List<DeveloperResponse>> ListDevelopersAsync();
    
    Task<DeveloperResponse> GetDeveloperByIdAsync(long id);
    
    Task<EnterpriseResponse> GetEnterpriseByIdAsync(long id);

    Task UpdateEnterpriseAsync(long id, EnterpriseUpdateRequest updateRequest);
    
    Task UpdateDeveloperAsync(long id, DeveloperUpdateRequest updateRequest);
    
    Task<DeveloperResponse> GetDeveloperByDevIdAsync(long developer_id);
}