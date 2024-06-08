using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebmasterAPI.Authentication.Domain.Services;
using WebmasterAPI.Projects.Domain.Models;
using WebmasterAPI.Projects.Domain.Services;
using WebmasterAPI.Projects.Domain.Services.Communication;
using WebmasterAPI.Projects.Resources;

namespace WebmasterAPI.Projects.Interfaces.Rest.Controllers;



[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public class DeliverableController : ControllerBase
{

    private readonly IDeliverableService _deliverableService;
    private readonly IMapper _mapper;
    
    public DeliverableController(IDeliverableService deliverableService, IMapper mapper)
    {
        _deliverableService = deliverableService;
        _mapper = mapper;
    }
    
    //Poner filtro 
    //GET: api/v1/Deliverables
    [HttpGet("api/v1/Deliverables")]
    public async Task<IActionResult> GetDeliverables()
    {
       var deliverables = await _deliverableService.ListDeliverablesAsync();
       return Ok(deliverables);
    }
    
    
    //GET: api/v1/Deliverables/{id}
    [HttpGet("api/v1/Deliverables/{id}")]
    public async Task<IActionResult> GetDeliverableById(long id)
    {
        var deliverable = await _deliverableService.GetDeliverableByIdAsync(id);
        return Ok(deliverable);
    }
    
    //DELETE: api/v1/Deliverables/{id}
    [HttpDelete("api/v1/Deliverables/{id}")]
    public async Task<IActionResult> DeleteDeliverableById(long id)
    {
        var response = await _deliverableService.DeleteDeliverableByIdAsync(id);
        return Ok(response);
    }
    
    //PUT: api/v1/Deliverables/{id}
    [HttpPut("api/v1/Deliverables/{id}")]
    public async Task<IActionResult> UpdateDeliverable(long id, [FromBody] DeliverableUpdateRequest resource)
    {
        var updateRequest = _mapper.Map<DeliverableUpdateRequest>(resource);
        await _deliverableService.UpdateDeliverableAsync(id, updateRequest);
        return Ok();
    }
    
    //POST: api/v1/Deliverables
    [HttpPost("api/v1/Deliverables")]
    public async Task<IActionResult> CreateDeliverable([FromBody] CreateDeliverableRequest request)
    {
        try
        {
            await _deliverableService.AddDeliverableAsync(request);
            return Ok(new { message = "Deliverable agregado exitosamente." });
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.InnerException?.Message ?? e.Message });
        }
    }
    
    
    //GET: api/v1/Projects/{projectId}/Deliverables
    [HttpGet("api/v1/Projects/{projectId}/Deliverables")]
    public async Task<IActionResult> GetDeliverableByProjectId(long projectId)
    {
        var deliverables = await _deliverableService.GetDeliverableByProjectIdAsync(projectId);
        return Ok(deliverables);
    }
    
    // POST: api/v1/Projects/{projectId}/Deliverables
    [HttpPost("api/v1/Projects/{projectId}/Deliverables")]
    public async Task<IActionResult> CreateDeliverableForProject(long projectId, [FromBody] CreateDeliverableByProjectIdRequest request)
    {
        try
        {
            await _deliverableService.AddDeliverableToProjectAsync(projectId, request);
            return Ok(new { message = "Deliverable added successfully to the project." });
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.InnerException?.Message ?? e.Message });
        }
    }
    
}