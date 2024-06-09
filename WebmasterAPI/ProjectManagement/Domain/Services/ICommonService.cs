namespace WebmasterAPI.ProjectManagement.Domain.Services;

public interface ICommonService<T, TI, TU, TDP> //T:dto, TI: insertDto, TU:updateDto TDP: DeveloperProject
{
    public List<string> Errors { get; }
    Task<IEnumerable<T>> Get();
    Task<T> GetById(long id);
    Task<T> Add(TI insertDto);
    Task<T> Update(long id, TU updateDto);
    Task<T> Delete(long id);
    bool Validate(TI insertDto);
    bool Validate(TU updateDto);
    Task<T> AssignDeveloper(long id, TDP insertDeveloperProjectDto);
}