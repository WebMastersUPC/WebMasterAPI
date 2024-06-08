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

    public void Update(Project project)
    {
        _appDbContext.Projects.Attach(project);
        _appDbContext.Projects.Entry(project).State = EntityState.Modified;
    }

    public void Delete(Project project)
    {
        _appDbContext.Projects.Remove(project);
    }

    public async Task Save() =>
        await _appDbContext.SaveChangesAsync();
    public IEnumerable<Project> Search(Func<Project, bool> filter) =>
        _appDbContext.Projects.Where(filter).ToList();
}