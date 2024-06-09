using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebmasterAPI.Support.Domain.Models;
using WebmasterAPI.Support.Domain.Services;
using WebmasterAPI.Support.Mapping;
using WebMasterApiSupport.Support.Resources;
using WebmasterAPI.Support.Resources;

namespace WebmasterAPI.Support.Interfaces.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportRequestController : ControllerBase
    {
        private readonly ISupportRequestService _supportRequestService;
        private readonly IMapper _mapper;

        public SupportRequestController(ISupportRequestService supportRequestService, IMapper mapper)
        {
            _supportRequestService = supportRequestService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<SupportRequestResource>> GetAllAsync()
        {
            var supportRequests = await _supportRequestService.ListAsync();
            var resources = _mapper.Map<IEnumerable<SupportRequest>, IEnumerable<SupportRequestResource>>(supportRequests);

            return resources;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveSupportRequestResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var supportRequest = _mapper.Map<SaveSupportRequestResource, SupportRequest>(resource);
            var result = await _supportRequestService.SaveAsync(supportRequest);

            var supportRequestResource = _mapper.Map<SupportRequest, SupportRequestResource>(result);

            return Ok(supportRequestResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveSupportRequestResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var supportRequest = _mapper.Map<SaveSupportRequestResource, SupportRequest>(resource);
            var result = await _supportRequestService.UpdateAsync(id, supportRequest);

            var supportRequestResource = _mapper.Map<SupportRequest, SupportRequestResource>(result);

            return Ok(supportRequestResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _supportRequestService.DeleteAsync(id);

            var supportRequestResource = _mapper.Map<SupportRequest, SupportRequestResource>(result);

            return Ok(supportRequestResource);
        }
    }
}
