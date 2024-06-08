using AutoMapper;
using WebmasterAPI.ProjectManagement.Domain.Models;
using WebmasterAPI.ProjectManagement.Domain.Repositories;
using WebmasterAPI.ProjectManagement.Domain.Services;
using WebmasterAPI.ProjectManagement.Domain.Services.Communication;
using WebmasterAPI.Authentication.Domain.Repositories;

namespace WebmasterAPI.ApiProject.Services;

public class ProjectService : ICommonService<ProjectDto, InsertProjectDto, UpdateProjectDto>
{
    private IProjectRepository<Project> _projectRepository;
    private IMapper _mapper;
    private IDeveloperRepository _developerRepository;
    public List<string> Errors { get; }

    public ProjectService(IProjectRepository<Project> projectRepository, IMapper mapper, 
        IDeveloperRepository developerRepository)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
        _developerRepository = developerRepository;
        Errors = new List<string>();
    }
    public async Task<IEnumerable<ProjectDto>> Get()
    {
        var projects = await _projectRepository.Get();
        return projects.Select(p => _mapper.Map<ProjectDto>(p));
    }

    public async Task<ProjectDto> GetById(long id)
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

    public async Task<ProjectDto> Update(long id, UpdateProjectDto updateDto)
    {
        if (await ValidateDeveloperIdsAsync(updateDto.developer_id))
        {
            var project = await _projectRepository.GetById(id);
            if (project != null)
            {
                _mapper.Map<UpdateProjectDto,Project>(updateDto, project);
                _projectRepository.Update(project);
                await _projectRepository.Save();
                var projectDto = _mapper.Map<ProjectDto>(project);
                return projectDto;
            }
            throw new Exception("Project not found.");
        }
        throw new Exception("One or more developer IDs are invalid.");
    }

    public async Task<ProjectDto> Delete(long id)
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
    public bool Validate(InsertProjectDto insertDto)
    {
        if (_projectRepository.Search(p => p.nameProject == insertDto.nameProject).Count() > 0)
        {
            Errors.Add("A project with the same name cannot exist");
            return false;
        }

        return true;
    }

    public bool Validate(UpdateProjectDto updateDto)
    {
        if (_projectRepository.Search(p => p.nameProject == updateDto.nameProject && 
                                        updateDto.projectID != p.projectID).Count() > 0)
        {
            Errors.Add("A project with the same name cannot exist");
            return false;
        }
        return true;
    }
}