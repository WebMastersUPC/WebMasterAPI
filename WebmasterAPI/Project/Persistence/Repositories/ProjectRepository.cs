using Microsoft.EntityFrameworkCore;
using WebmasterAPI.Project.Domain.Repositories;
using WebmasterAPI.Project.Domain.Services.Communication;
using WebmasterAPI.Shared.Persistence.Contexts;

namespace WebmasterAPI.Project.Persistence.Repositories;

public class ProjectRepository : IProjectRepository<ProjectDto>
{
    private AppDbContext _appDbContext;

    public ProjectRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<ProjectDto>> Get()
    {
        throw new NotImplementedException();
    }

    public Task<ProjectDto> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task Add(ProjectDto entity)
    {
        throw new NotImplementedException();
    }

    public void Update(ProjectDto entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(ProjectDto entity)
    {
        throw new NotImplementedException();
    }

    public Task Save()
    {
        throw new NotImplementedException();
    }
}