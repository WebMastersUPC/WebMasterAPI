namespace WebmasterAPI.ProjectManagement.Domain.Repositories;

public interface IProjectRepository<TEntity>
{
    Task<IEnumerable<TEntity>> Get();
    Task<TEntity> GetById(long id);
    Task Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task Save();
    IEnumerable<TEntity> Search(Func<TEntity, bool> filter);
    Task<IEnumerable<TEntity>> GetAvailableProjects();
    Task<IEnumerable<TEntity>> GetProjectByDeveloperId(long developerId);
    Task<IEnumerable<TEntity>> GetProjectByEnterpriseId(long enterpriseId);


}
