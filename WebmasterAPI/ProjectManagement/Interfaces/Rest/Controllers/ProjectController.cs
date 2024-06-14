using FluentValidation;
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
        private ICommonService<ProjectDto, InsertProjectDto, UpdateProjectDto,InsertDeveloperProjectDto> _projectService;
        private IValidator<InsertProjectDto> _projectInsertValidation;
        private IValidator<UpdateProjectDto> _projectUpdateValidation;

        public ProjectController([FromKeyedServices("projectService")]
            ICommonService<ProjectDto, InsertProjectDto, UpdateProjectDto, InsertDeveloperProjectDto> projectService,
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
        public async Task<ActionResult<ProjectDto>> GetById(long id)
        {
            var projectDto = await _projectService.GetById(id);
            return projectDto == null ? NotFound() : Ok(projectDto);
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
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProjectDto>> Delete(long id)
        {
            var projectDto = await _projectService.Delete(id);
            return projectDto == null ? NotFound() : Ok(projectDto);
        }

        [HttpPost("assign-developer/{id}")]
        public async Task<IActionResult> AssignDeveloper(long id, InsertDeveloperProjectDto insertDeveloperProjectDto)
        {
            var project = await _projectService.AssignDeveloper(id, insertDeveloperProjectDto);
            return project == null ? NotFound("Project not found or developer not an applicant") : 
                Ok(new { message = "Added developer succesful", status = "200"});
        }

        [HttpPost("add-applicant/{id}")]
        public async Task<IActionResult> AddApplicant(long id, InsertDeveloperProjectDto insertDeveloperProjectDto)
        {
            var project = await _projectService.AddApplicant(id, insertDeveloperProjectDto);
            return project == null ? NotFound("Project not found or applicant not exist") :  
                Ok(new { message = "Added developer succesful", status = "200"});
        }
    }
}
