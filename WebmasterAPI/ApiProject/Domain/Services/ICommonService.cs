namespace WebmasterAPI.ApiProject.Domain.Services;

public interface ICommonService<T, TI, TU> //T:dto, TI: insertDto, TU:updateDto
{
    public List<string> Errors { get; }
    Task<IEnumerable<T>> Get();
    Task<T> GetById(int id);
    Task<T> Add(TI insertDto);
    Task<T> Update(int id, TU updateDto);
    Task<T> Delete(int id);
    bool Validate(TI insertDto);
    bool Validate(TU updateDto);
    
}