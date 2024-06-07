using AutoMapper;
using WebmasterAPI.ApiProject.Domain.Models;
using WebmasterAPI.ApiProject.Domain.Repositories;
using WebmasterAPI.ApiProject.Domain.Services;
using WebmasterAPI.ApiProject.Domain.Services.Communication;

namespace WebmasterAPI.ApiProject.Services;

public class ProjectService : ICommonService<ProjectDto, InsertProjectDto, UpdateProjectDto>
{
    private IProjectRepository<Project> _projectRepository;
    private IMapper _mapper;

    public ProjectService(IProjectRepository<Project> projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
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

    public Task<ProjectDto> Add(InsertProjectDto insertDto)
    {
        throw new NotImplementedException();
    }

    public Task<ProjectDto> Update(int id, UpdateProjectDto updateDto)
    {
        throw new NotImplementedException();
    }

    public Task<ProjectDto> Delete(int id)
    {
        throw new NotImplementedException();
    }
}