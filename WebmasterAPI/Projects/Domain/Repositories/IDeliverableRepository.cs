using WebmasterAPI.Projects.Domain.Models;

namespace WebmasterAPI.Projects.Domain.Repositories;

public interface IDeliverableRepository
{

    Task AddSync(Deliverable deliverable);
    
    Task UpdateAsync(Deliverable deliverable);
    
    Task<Deliverable> FindDeliverableIdAsync(long id);

    Task <List<Deliverable>> ListAsync();

    void Remove(Deliverable deliverable);

    Task<bool> ExistByIdAsync(long id);

}