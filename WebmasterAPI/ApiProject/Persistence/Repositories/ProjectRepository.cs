using Microsoft.EntityFrameworkCore;
using WebmasterAPI.ApiProject.Domain.Models;
using WebmasterAPI.ApiProject.Domain.Repositories;
using WebmasterAPI.Shared.Persistence.Contexts;

namespace WebmasterAPI.ApiProject.Persistence.Repositories;

public class ProjectRepository : IProjectRepository<Project>
{
    private AppDbContext _appDbContext;

    public ProjectRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<Project>> Get() =>
        await _appDbContext.Projects.ToListAsync();

    public async Task<Project> GetById(int id) =>
        await _appDbContext.Projects.FindAsync(id);

    public async Task Add(Project project) =>
        await _appDbContext.Projects.AddAsync(project);

    public void Update(Project entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Project entity)
    {
        throw new NotImplementedException();
    }

    public Task Save()
    {
        throw new NotImplementedException();
    }
}