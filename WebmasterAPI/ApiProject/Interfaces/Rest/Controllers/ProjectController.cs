using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebmasterAPI.ApiProject.Domain.Services;
using WebmasterAPI.ApiProject.Domain.Services.Communication;

namespace WebmasterAPI.ApiProject.Interfaces.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private ICommonService<ProjectDto, InsertProjectDto, UpdateProjectDto> _projectService;

        public ProjectController([FromKeyedServices("projectService")]
            ICommonService<ProjectDto, InsertProjectDto, UpdateProjectDto> projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProjectDto>> Get() =>
            await _projectService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetById(int id)
        {
            var projectDto = await _projectService.GetById(id);
            return projectDto == null ? NotFound() : Ok(projectDto);
        }

        [HttpPost]
        public async Task<ActionResult<InsertProjectDto>> Add(InsertProjectDto insertProjectDto)
        {
            var projectDto = await _projectService.Add(insertProjectDto);
            return CreatedAtAction(nameof(GetById), new { id = projectDto.Id }, projectDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProjectDto>> Delete(int id)
        {
            var projectDto = await _projectService.Delete(id);
            return projectDto == null ? NotFound() : Ok(projectDto);
        }
    }
}
