using AutoMapper;
using WebmasterAPI.ApiProject.Domain.Models;
using WebmasterAPI.ApiProject.Domain.Repositories;
using WebmasterAPI.ApiProject.Domain.Services;
using WebmasterAPI.ApiProject.Domain.Services.Communication;
using WebmasterAPI.Authentication.Domain.Repositories;

namespace WebmasterAPI.ApiProject.Services;

public class ProjectService : ICommonService<ProjectDto, InsertProjectDto, UpdateProjectDto>
{
    private IProjectRepository<Project> _projectRepository;
    private IMapper _mapper;
    private IDeveloperRepository _developerRepository;

    public ProjectService(IProjectRepository<Project> projectRepository, IMapper mapper, 
        IDeveloperRepository developerRepository)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
        _developerRepository = developerRepository;
    }
    public async Task<IEnumerable<ProjectDto>> Get()
    {
        var projects = await _projectRepository.Get();
        return projects.Select(p => _mapper.Map<ProjectDto>(p));
    }

    public async Task<ProjectDto> GetById(int id)
    {
        var project = await _projectRepository.GetById(id);
        if (project != null)
        {
            var projectDto = _mapper.Map<ProjectDto>(project);
            return projectDto;
        }

        return null;
    }
    public async Task<bool> ValidateDeveloperIdsAsync(List<long> developerIds)
    {
        var existingDeveloperIds = await _developerRepository.GetAllDeveloperIdsAsync();
        return developerIds.All(id => existingDeveloperIds.Contains(id));
    }
    public async Task<ProjectDto> Add(InsertProjectDto insertDto)
    {
        if (await ValidateDeveloperIdsAsync(insertDto.developer_id))
        {
            var project = _mapper.Map<Project>(insertDto);
            await _projectRepository.Add(project);
            
            await _projectRepository.Save();
            var projectDto = _mapper.Map<ProjectDto>(project);
            return projectDto;
        }
        throw new Exception("One or more developer IDs are invalid.");
    }

    public Task<ProjectDto> Update(int id, UpdateProjectDto updateDto)
    {
        throw new NotImplementedException();
    }

    public async Task<ProjectDto> Delete(int id)
    {
        var project = await _projectRepository.GetById(id);
        if (project != null)
        {
            var projectDto = _mapper.Map<ProjectDto>(project);
            _projectRepository.Delete(project);
            await _projectRepository.Save();
            return projectDto;
        }

        return null;
    }
}