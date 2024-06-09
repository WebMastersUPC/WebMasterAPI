using WebmasterAPI.Support.Domain.Models;

namespace WebmasterAPI.Support.Domain.Services
{
    public interface ISupportRequestService
    {
        Task<IEnumerable<SupportRequest>> ListAsync();
        Task<SupportRequest> FindByIdAsync(int id);
        Task<SupportRequest> SaveAsync(SupportRequest supportRequest);
        Task<SupportRequest> UpdateAsync(int id, SupportRequest supportRequest);
        Task<SupportRequest> DeleteAsync(int id);
    }
}
