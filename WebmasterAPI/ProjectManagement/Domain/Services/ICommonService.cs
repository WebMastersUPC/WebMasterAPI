namespace WebmasterAPI.ProjectManagement.Domain.Services;

public interface ICommonService<T, TI, TU> //T:dto, TI: insertDto, TU:updateDto
{
    public List<string> Errors { get; }
    Task<IEnumerable<T>> Get();
    Task<T> GetById(long id);
    Task<T> Add(TI insertDto);
    Task<T> Update(long id, TU updateDto);
    Task<T> Delete(long id);
    bool Validate(TI insertDto);
    bool Validate(TU updateDto);
    
}