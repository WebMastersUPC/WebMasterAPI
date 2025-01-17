using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebmasterAPI.Authentication.Domain.Services;
using WebmasterAPI.Authentication.Domain.Services.Communication;
using WebmasterAPI.Authentication.Resources;
using WebmasterAPI.UserManagement.Domain.Models;

namespace WebmasterAPI.Controllers;

[Authorize]
[Route("api/v1/[controller]")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;
    private readonly IMapper _mapper;
    
    
    public ProfileController(IProfileService profileService, IMapper mapper)
    {
        _profileService = profileService;
        _mapper = mapper;
    }
    
    // GET: api/v1/Profile/Developers
    [HttpGet("Developers")]
    public async Task<IActionResult> GetDevelopers()
    {
        var developers = await _profileService.ListDevelopersAsync();
        return Ok(developers);
    }
    
    // GET: api/v1/Profile/Developers/{id}
    [HttpGet("Developers/{id}")]
    public async Task<IActionResult> GetDeveloperById(long id)
    {
        var developer = await _profileService.GetDeveloperByIdAsync(id);
        return Ok(developer);
    }
    
    // GET: api/v1/Profile/Enterprises/{id}
    [HttpGet("Enterprises/{id}")]
    public async Task<IActionResult> GetEnterpriseById(long id)
    {
        var enterprise = await _profileService.GetEnterpriseByIdAsync(id);
        return Ok(enterprise);
    }
    
    // GET: api/v1/Profile/Enterprises/{developer_id}
    [HttpGet("Profile/Developers/{developer_id}")]
    public async Task<IActionResult> GetDeveloperByDevId(long developer_id)
    {
        var developer = await _profileService.GetDeveloperByDevIdAsync(developer_id);
        var developerSummary = _mapper.Map<DevApplicant>(developer);
        return Ok(developerSummary);
    }
    
    // GET: api/v1/Profile/Enterprises/{enterprise_id}
    [HttpGet("Profile/Enterprises/{enterprise_id}")]
    public async Task<IActionResult> GetEnterpriseByEnterpriseId(long enterprise_id)
    {
        var enterprise = await _profileService.GetEnterpriseByEnterpriseIdAsync(enterprise_id);
        var enterpriseSummary = _mapper.Map<EnterpriseProfile>(enterprise);
        return Ok(enterpriseSummary);
    }
    
    // PUT: api/v1/Profile/Enterprises/{id}
    [HttpPut("Enterprises/{id}")]
    public async Task<IActionResult> UpdateEnterprise(long id, [FromBody] EnterpriseUpdateRequest resource)
    {
        var updateRequest = _mapper.Map<EnterpriseUpdateRequest>(resource);
        await _profileService.UpdateEnterpriseAsync(id, updateRequest);
        return Ok();
    }
    
    // PUT: api/v1/Profile/Developers/{id}
    [HttpPut("Developers/{id}")]
    public async Task<IActionResult> UpdateDeveloper(long id, [FromBody] DeveloperUpdateRequest resource)
    {
        var updateRequest = _mapper.Map<DeveloperUpdateRequest>(resource);
        await _profileService.UpdateDeveloperAsync(id, updateRequest);
        return Ok();
    }
    
    // PUT: api/v1/Profile/Developers/{developer_id}/img
    [HttpPut("Developers/{id}/img")]
    public async Task<IActionResult> UpdateDeveloperProfileImg(long id, [FromBody] DevImgUpdateRequest resource)
    {
        var updateRequest = _mapper.Map<DevImgUpdateRequest>(resource);
        await _profileService.UpdateDeveloperProfileImgAsync(id, updateRequest);
        return Ok();
    }
    
    // PUT: api/v1/Profile/Enterprises/{enterprise_id}/img
    [HttpPut("Enterprises/{id}/img")]
    public async Task<IActionResult> UpdateEnterpriseProfileImg(long id, [FromBody] EnterpriseImgUpdateRequest resource)
    {
        var updateRequest = _mapper.Map<EnterpriseImgUpdateRequest>(resource);
        await _profileService.UpdateEnterpriseProfileImgAsync(id, updateRequest);
        return Ok();
    }
    
}