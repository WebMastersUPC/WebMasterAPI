using WebmasterAPI.Authentication.Domain.Models;
using WebmasterAPI.Authentication.Domain.Services.Communication;

namespace WebmasterAPI.Authentication.Domain.Repositories;

public interface IDeveloperRepository
{
    Task AddAsync(Developer developer);
    Task<Developer> FindByIdAsync(long id);
    void Remove(Developer developer);
    Task<List<Developer>> ListAsync();
    Task UpdateAsync(Developer developer);
    Task<List<long>> GetAllDeveloperIdsAsync();
}