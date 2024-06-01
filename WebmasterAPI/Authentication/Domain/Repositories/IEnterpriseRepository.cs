using WebmasterAPI.Authentication.Domain.Models;

namespace WebmasterAPI.Authentication.Domain.Repositories;

public interface IEnterpriseRepository
{
    Task AddAsync(Enterprise enterprise);
    Task<Enterprise> FindByIdAsync(int id);
    void Remove(Enterprise enterprise);
}