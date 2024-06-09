using WebmasterAPI.Support.Domain.Models;

namespace WebmasterAPI.Support.Domain.Repositories
{
    public interface ISupportRequestRepository
    {
        Task<IEnumerable<SupportRequest>> ListAsync();
        Task<SupportRequest> FindByIdAsync(int id);
        Task AddAsync(SupportRequest supportRequest);
        void Update(SupportRequest supportRequest);
        void Remove(SupportRequest supportRequest);
    }
}
