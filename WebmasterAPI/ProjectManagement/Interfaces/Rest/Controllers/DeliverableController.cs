using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebmasterAPI.Authentication.Domain.Services;
using WebmasterAPI.ProjectManagement.Domain.Models;
using WebmasterAPI.ProjectManagement.Domain.Services;
using WebmasterAPI.ProjectManagement.Domain.Services.Communication;
using WebmasterAPI.ProjectManagement.Resources;

namespace WebmasterAPI.ProjectManagement.Interfaces.Rest.Controllers;



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
            return Ok(new { message = "Entregable añadido exitosamente en el proyecto." });
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.InnerException?.Message ?? e.Message });
        }
    }
    
    //PUT: api/v1/Projects/{projectId}/Deliverables/{deliverableId}
    [HttpPut("api/v1/Projects/{projectId}/Deliverables/{orderNumber}")]
    public async Task<IActionResult> UpdateDeliverableByProjectIdandDeliverableId(long projectId, int orderNumber, [FromBody] DeliverableUpdateRequest resource)
    {
        
        var updateRequest = _mapper.Map<DeliverableUpdateRequest>(resource);
        await _deliverableService.UpdateDeliverableByProjectIdandDeliverableIdAsync(projectId, orderNumber, updateRequest);
        return Ok(new { message = "El entregable ha sido modificado." });
    }
    
    //DELETE: api/v1/Projects/{projectId}/Deliverables/{deliverableId}
    [HttpDelete("api/v1/Projects/{projectId}/Deliverables/{orderNumber}")]
    public async Task<IActionResult> DeleteDeliverableByProjectIdandDeliverableId(long projectId, int orderNumber)
    {
        var response = await _deliverableService.DeleteDeliverableByProjectIdandDeliverableIdAsync(projectId, orderNumber);
        return Ok(response);
    }
    
    // POST: api/v1/Projects/{projectId}/Deliverables/{deliverableId}/Developers/{developerId}/Upload
    [HttpPost("api/v1/Projects/{projectId}/Deliverables/{orderNumber}/Upload")]
    public async Task<IActionResult> UploadDeliverableAsync(long projectId, int orderNumber, long developerId, [FromBody] UploadDeliverableRequest upload)
    {
        try
        {
            var response = await _deliverableService.UploadDeliverableAsync(projectId, orderNumber, developerId, upload);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.InnerException?.Message ?? e.Message });
        }
    }

    
    // PUT: api/v1/Deliverables/{deliverableId}/Approve
    [HttpPut("api/v1/Projects/{projectId}/Deliverables/{orderNumber}/Approve")]
    public async Task<IActionResult> ApproveDeliverable(int orderNumber)
    {
        try
        {
            await _deliverableService.ApproveOrRejectDeliverableAsync(orderNumber, "Aprobado");
            return Ok(new { message = "El entregable ha sido aprobado." });
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.InnerException?.Message ?? e.Message });
        }
    }

    // PUT: api/v1/Deliverables/{deliverableId}/Reject
    [HttpPut("api/v1/Projects/{projectId}/Deliverables/{orderNumber}/Reject")]
    public async Task<IActionResult> RejectDeliverable(int orderNumber)
    {
        try
        {
            await _deliverableService.ApproveOrRejectDeliverableAsync(orderNumber, "Rechazado");
            return Ok(new { message = "El entregable ha sido rechazado." });
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.InnerException?.Message ?? e.Message });
        }
    }
    
    // GET: api/v1/Projects/{projectId}/Deliverables/{deliverableId}/Uploaded
    [HttpGet("api/v1/Projects/{projectId}/Deliverables/{orderNumber}/Review")]
    public async Task<IActionResult> GetUploadedDeliverableByProjectIdAndDeliverableId(long projectId, int orderNumber)
    {
        var deliverable = await _deliverableService.GetUploadedDeliverableByProjectIdAndDeliverableIdAsync(projectId, orderNumber);

        if (deliverable == null)
        {
            return NotFound("No se encontró el entregable subido para este proyecto.");
        }

        return Ok(deliverable);
    }
    
    

}