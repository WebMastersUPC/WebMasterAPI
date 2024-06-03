using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebmasterAPI.Authentication.Domain.Services;
using WebmasterAPI.Projects.Domain.Services;

namespace WebmasterAPI.Projects.Interfaces.Rest.Controllers;

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
    [HttpGet("Deliverables")]
    public async Task<IActionResult> GetDeliverables()
    {
       var deliverables = await _deliverableService.ListDeliverablesAsync();
       return Ok(deliverables);
    }
    
    
    //GET: api/v1/Deliverables/{id}
    [HttpGet("Deliverables/{id}")]
    public async Task<IActionResult> GetDeliverableById(long id)
    {
        var deliverable = await _deliverableService.GetDeliverableByIdAsync(id);
        return Ok(deliverable);
    }
    
    //DELETE: api/v1/Deliverables/{id}
    [HttpDelete("Deliverables/{id}")]
    public async Task<IActionResult> DeleteDeliverableById(long id)
    {
        var response = await _deliverableService.DeleteDeliverableByIdAsync(id);
        return Ok(response);
    }
    
    
}