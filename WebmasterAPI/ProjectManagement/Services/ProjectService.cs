using AutoMapper;
using WebmasterAPI.ProjectManagement.Domain.Models;
using WebmasterAPI.ProjectManagement.Domain.Repositories;
using WebmasterAPI.ProjectManagement.Domain.Services;
using WebmasterAPI.ProjectManagement.Domain.Services.Communication;
using WebmasterAPI.Authentication.Domain.Repositories;

namespace WebmasterAPI.ProjectManagement.Services;

public class ProjectService : ICommonService<ProjectDto, InsertProjectDto, UpdateProjectDto, InsertDeveloperProjectDto>
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
    public async Task<IEnumerable<ProjectDto>> GetAvailableProjects()
    {
        var projects = await _projectRepository.GetAvailableProjects();
        return projects.Select(p => _mapper.Map<ProjectDto>(p));
    }

    public async Task<ProjectDto> Add(InsertProjectDto insertDto)
    {
       
            var project = _mapper.Map<Project>(insertDto);
            await _projectRepository.Add(project);
            
            await _projectRepository.Save();
            var projectDto = _mapper.Map<ProjectDto>(project);
            return projectDto;
      
    }
    public async Task<bool> ValidateDeveloperIdsAsync(List<long> applicantsId, long developerId)
    {
        var existingDeveloperIds = await _developerRepository.GetAllDeveloperIdsAsync();
        var allValid = applicantsId.All(id => existingDeveloperIds.Contains(id));
        var developerInApplicants = applicantsId.Contains(developerId);

        return allValid && developerInApplicants;
    }
    public async Task<ProjectDto> Update(long id, UpdateProjectDto updateDto)
    {
        if (await ValidateDeveloperIdsAsync(updateDto.applicants_id, updateDto.developer_id) )
        {
            var project = await _projectRepository.GetById(id);
            if (project != null)
            {
                _mapper.Map<UpdateProjectDto, Project>(updateDto, project);
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
                                        updateDto.project_ID != p.projectID).Count() > 0)
        {
            Errors.Add("A project with the same name cannot exist");
            return false;
        }
        return true;
    }
    
    public async Task<bool> ValidateDeveloperIdForProjectAsync(long projectId, long developerId)
    {
        var project = await _projectRepository.GetById(projectId);
        if (project == null)
        {
            throw new Exception("Project not found.");
        }

        // Registro para depuración
        Console.WriteLine($"Project ID: {projectId}, Developer ID: {developerId}");
        Console.WriteLine($"Applicants ID: {string.Join(", ", project.applicants_id)}");

        return project.applicants_id.Contains(developerId);
    }
    public async Task<ProjectDto> AssignDeveloper(long projectId, InsertDeveloperProjectDto insertDeveloperProjectDto)
    {
        var project = await _projectRepository.GetById(projectId);
        if (project == null)
        {
            throw new Exception("Project not found.");
        }
        
        if (project.developer_id != null)
        {
            throw new Exception("There is already a developer assigned to this project.");
        }
        
        if (!await ValidateDeveloperIdForProjectAsync(projectId, insertDeveloperProjectDto.developer_id))
        {
            throw new Exception("Developer ID must be one of the applicants.");
        }

        project.developer_id = insertDeveloperProjectDto.developer_id;
        project.applicants_id.Remove(insertDeveloperProjectDto.developer_id);
        _mapper.Map<InsertDeveloperProjectDto, Project>(insertDeveloperProjectDto, project);
        _projectRepository.Update(project);
        await _projectRepository.Save();
        var projectDto = _mapper.Map<ProjectDto>(project);
        return projectDto;
    }
    
    public async Task<ProjectDto> AddApplicant(long projectId, InsertDeveloperProjectDto insertDeveloperProjectDto)
    {
        var project = await _projectRepository.GetById(projectId);
        if (project == null)
        {
            throw new Exception("Project not found.");
        }
        
        if (project.applicants_id == null)
        {
            project.applicants_id = new List<long>();
        }
        
        if (project.applicants_id.Contains(insertDeveloperProjectDto.developer_id))
        {
            throw new Exception("Applicant already exists in the project.");
        }
        
        project.applicants_id.Add(insertDeveloperProjectDto.developer_id);
        _projectRepository.Update(project);
        await _projectRepository.Save();
        var projectDto = _mapper.Map<ProjectDto>(project);
        return projectDto;
    }
    public async Task<ProjectDto> DeleteApplicant(long projectId, InsertDeveloperProjectDto insertDeveloperProjectDto)
    {
        var project = await _projectRepository.GetById(projectId);
        if (project == null)
        {
            throw new Exception("Project not found.");
        }

        if (project.applicants_id == null || !project.applicants_id.Contains(insertDeveloperProjectDto.developer_id))
        {
            throw new Exception("Applicant not found in the project.");
        }

        project.applicants_id.Remove(insertDeveloperProjectDto.developer_id);
        _projectRepository.Update(project);
        await _projectRepository.Save();
        var projectDto = _mapper.Map<ProjectDto>(project);
        return projectDto;
    }
    public async Task<ProjectDto> DeleteDeveloper(long projectId, InsertDeveloperProjectDto insertDeveloperProjectDto)
    {
        var project = await _projectRepository.GetById(projectId);
        if (project == null)
        {
            throw new Exception("Project not found.");
        }

        if (project.developer_id != insertDeveloperProjectDto.developer_id)
        {
            throw new Exception("Developer not found in the project.");
        }

        project.developer_id = null; // Remove the developer
        _projectRepository.Update(project);
        await _projectRepository.Save();
        var projectDto = _mapper.Map<ProjectDto>(project);
        return projectDto;
    }
}