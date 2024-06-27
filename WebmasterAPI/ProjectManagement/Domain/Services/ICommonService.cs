namespace WebmasterAPI.ProjectManagement.Domain.Services;

public interface ICommonService<T, TI, TU, TDP, HDP,HEP,PDP,AP> 
    /*T:Projectdto, TI: insertDto, TU:updateDto TDP: DeveloperProject, HDP:HomeDeveloperProjectDto,
     HEP:HomeDeveloperProjectDto, PDP:PostulateDeveloperProjectDto, AP:AvailableProjectDto*/
{
    public List<string> Errors { get; }
    Task<IEnumerable<T>> Get();
    Task<PDP> GetById(long id);
    Task<T> Add(TI insertDto);
    Task<T> Update(long id, TU updateDto);
    Task<T> Delete(long id);
    bool Validate(TI insertDto);
    bool Validate(TU updateDto);
    Task<T> AssignDeveloper(long id, TDP insertDeveloperProjectDto);
    Task<T> AddApplicant(long projectId, TDP insertDeveloperProjectDto);
    Task<T> DeleteApplicant(long projectId, TDP insertDeveloperProjectDto);
    Task<T> DeleteDeveloper(long projectId, TDP insertDeveloperProjectDto);
    Task<IEnumerable<AP>> GetAvailableProjects();
    Task<IEnumerable<HDP>> GetProjectByDeveloperId(long developerId);
    Task<IEnumerable<HEP>> GetProjectByEnterpriseId(long enterpriseId);
}