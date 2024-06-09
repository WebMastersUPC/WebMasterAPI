using Microsoft.EntityFrameworkCore;
using WebmasterAPI.Shared.Persistence.Contexts;
using WebmasterAPI.Shared.Persistence.Repositories;
using WebmasterAPI.Support.Domain.Models;
using WebmasterAPI.Support.Domain.Repositories;


namespace WebmasterAPI.Support.Persistence
{
    public class SupportRequestRepository : BaseRepository, ISupportRequestRepository
    {
        public SupportRequestRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<SupportRequest>> ListAsync()
        {
            return await _Context.SupportRequests.ToListAsync();
        }

        public async Task<SupportRequest> FindByIdAsync(int id)
        {
            return await _Context.SupportRequests.FindAsync(id);
        }

        public async Task AddAsync(SupportRequest supportRequest)
        {
            await _Context.SupportRequests.AddAsync(supportRequest);
        }

        public void Update(SupportRequest supportRequest)
        {
            _Context.SupportRequests.Update(supportRequest);
        }

        public void Remove(SupportRequest supportRequest)
        {
            _Context.SupportRequests.Remove(supportRequest);
        }
    }
}
