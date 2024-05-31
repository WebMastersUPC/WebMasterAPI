using WebmasterAPI.Authentication.Domain.Models;

namespace WebmasterAPI.Authentication.Domain.Repositories;

public interface IDeveloperRepository
{
    Task AddAsync(Developer developer);
    Task<Developer> FindByIdAsync(int id);
    void Remove(Developer developer);
}