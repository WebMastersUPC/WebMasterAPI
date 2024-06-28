using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebmasterAPI.ProjectManagement.Domain.Services;
using WebmasterAPI.ProjectManagement.Domain.Services.Communication;

namespace WebmasterAPI.ProjectManagement.Interfaces.Rest.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private ICommonService<ProjectDto, InsertProjectDto, UpdateProjectDto,InsertDeveloperProjectDto,
        HomeDeveloperProjectDto,HomeEnterpriseProjectDto,PostulateDeveloperProjectDto, AvailableProjectDto> _projectService;
        private IValidator<InsertProjectDto> _projectInsertValidation;
        private IValidator<UpdateProjectDto> _projectUpdateValidation;

        public ProjectController([FromKeyedServices("projectService")]
            ICommonService<ProjectDto, InsertProjectDto, UpdateProjectDto, InsertDeveloperProjectDto,
                HomeDeveloperProjectDto,HomeEnterpriseProjectDto,PostulateDeveloperProjectDto, AvailableProjectDto> projectService,
            IValidator<InsertProjectDto> projectInsertValidation, IValidator<UpdateProjectDto> projectUpdateValidation )
        {
            _projectService = projectService;
            _projectInsertValidation = projectInsertValidation;
            _projectUpdateValidation = projectUpdateValidation;
        }

        [HttpGet]
        public async Task<IEnumerable<ProjectDto>> Get() =>
            await _projectService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<PostulateDeveloperProjectDto>> GetById(long id)
        {
            var projectDto = await _projectService.GetById(id);
            return projectDto == null ? NotFound() : Ok(projectDto);
        }
        
        [HttpGet("available-projects")]
        public async Task<ActionResult<IEnumerable<AvailableProjectDto>>> GetAvailableProjects()
        {
            var projects = await _projectService.GetAvailableProjects();
            return projects == null ? NotFound() : Ok(projects);
        }
        
        [HttpGet("by-developer/{developerId}")]
        public async Task<ActionResult<IEnumerable<HomeDeveloperProjectDto>>> GetByDeveloperId(long developerId)
        {
            var projects = await _projectService.GetProjectByDeveloperId(developerId);
            return projects == null ? NotFound() : Ok(projects);
        }
        
        [HttpGet("by-enterprise/{enterpriseId}")]
        public async Task<ActionResult<IEnumerable<HomeEnterpriseProjectDto>>> GetByEnterpriseId(long enterpriseId)
        {
            var projects = await _projectService.GetProjectByEnterpriseId(enterpriseId);
            return projects == null ? NotFound() : Ok(projects);
        }
        
        [HttpPost]
        public async Task<ActionResult<InsertProjectDto>> Add(InsertProjectDto insertProjectDto)
        {
            var validationResult = await _projectInsertValidation.ValidateAsync(insertProjectDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            if (!_projectService.Validate(insertProjectDto))
            {
                return BadRequest(_projectService.Errors);
            }
            var projectDto = await _projectService.Add(insertProjectDto);
            return CreatedAtAction(nameof(GetById), new { id = projectDto.project_ID }, projectDto);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectDto>> Update(long id, UpdateProjectDto updateProjectDto)
        {
            var validationResult = await _projectUpdateValidation.ValidateAsync(updateProjectDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            if (!_projectService.Validate(updateProjectDto))
            {
                return BadRequest(_projectService.Errors);
            }
            var projectDto = await _projectService.Update(id, updateProjectDto);
            return projectDto == null ? NotFound() : Ok(projectDto);
        }
        
        [HttpPost("add-applicant/{id}")]
        public async Task<IActionResult> AddApplicant(long id, InsertDeveloperProjectDto insertDeveloperProjectDto)
        {
            var project = await _projectService.AddApplicant(id, insertDeveloperProjectDto);
            return project == null ? NotFound("Project not found or applicant not exist") :  
                Ok(new { message = "Added developer succesful", status = "200"});
        }
        
        [HttpPost("assign-developer/{id}")]
        public async Task<IActionResult> AssignDeveloper(long id, InsertDeveloperProjectDto insertDeveloperProjectDto)
        {
            var project = await _projectService.AssignDeveloper(id, insertDeveloperProjectDto);
            return project == null ? NotFound("Project not found or developer not an applicant") : 
                Ok(new { message = "Added developer succesful", status = "200"});
        }
        

        [HttpDelete("delete-applicant/{id}")]
        public async Task<IActionResult> DeleteApplicant(long id, InsertDeveloperProjectDto insertDeveloperProjectDto)
        {
            var project = await _projectService.DeleteApplicant(id, insertDeveloperProjectDto);
            return project == null ? NotFound("Project not found or applicant not exist") : 
                Ok(new { message = "Delete applicant succesful", status = "200"});
        }

        [HttpDelete("delete-developer/{id}")]
        public async Task<IActionResult> DeleteDeveloper(long id, InsertDeveloperProjectDto insertDeveloperProjectDto)
        {
            var project = await _projectService.DeleteDeveloper(id, insertDeveloperProjectDto);
            return project == null
                ? NotFound("Project not found or developer not exist")
                : Ok(new { message = "Delete developer succesful", status = "200" });
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProjectDto>> Delete(long id)
        {
            var projectDto = await _projectService.Delete(id);
            return projectDto == null ? NotFound() : Ok(projectDto);
        }
    }
}
