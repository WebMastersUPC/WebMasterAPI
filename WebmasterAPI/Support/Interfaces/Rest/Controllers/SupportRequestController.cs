using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebmasterAPI.Support.Domain.Models;
using WebmasterAPI.Support.Domain.Services;
using WebmasterAPI.Support.Resources;
using WebMasterApiSupport.Support.Resources;

namespace WebmasterAPI.Support.Interfaces.Rest.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SupportRequestController : ControllerBase
    {
        private readonly ISupportRequestService _supportRequestService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public SupportRequestController(ISupportRequestService supportRequestService, IMapper mapper, IWebHostEnvironment env)
        {
            _supportRequestService = supportRequestService;
            _mapper = mapper;
            _env = env;
        }

        [HttpGet]
        public async Task<IEnumerable<SupportRequestResource>> GetAllAsync()
        {
            var supportRequests = await _supportRequestService.ListAsync();
            var resources = _mapper.Map<IEnumerable<SupportRequest>, IEnumerable<SupportRequestResource>>(supportRequests);
            return resources;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostAsync([FromForm] SaveSupportRequestResource resource)
        {
            var userId = User.FindFirst(ClaimTypes.Sid)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            resource.UserId = int.Parse(userId);

            var supportRequest = _mapper.Map<SaveSupportRequestResource, SupportRequest>(resource);

            if (resource.Attachment != null)
            {
                var webRootPath = _env.WebRootPath ?? "";
                var attachmentsPath = Path.Combine(webRootPath, "Attachments");

                if (!Directory.Exists(attachmentsPath))
                {
                    Directory.CreateDirectory(attachmentsPath);
                }

                var filePath = Path.Combine(attachmentsPath, resource.Attachment.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await resource.Attachment.CopyToAsync(stream);
                }

                supportRequest.AttachmentPath = filePath;
            }

            var result = await _supportRequestService.SaveAsync(supportRequest);
            var supportRequestResource = _mapper.Map<SupportRequest, SupportRequestResource>(result);

            return Ok(supportRequestResource);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutAsync(int id, [FromForm] SaveSupportRequestResource resource)
        {
            var userId = User.FindFirst(ClaimTypes.Sid)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            resource.UserId = int.Parse(userId);

            var supportRequest = _mapper.Map<SaveSupportRequestResource, SupportRequest>(resource);

            if (resource.Attachment != null)
            {
                var filePath = Path.Combine("Attachments", resource.Attachment.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await resource.Attachment.CopyToAsync(stream);
                }
                supportRequest.AttachmentPath = filePath;
            }

            var result = await _supportRequestService.UpdateAsync(id, supportRequest);
            var supportRequestResource = _mapper.Map<SupportRequest, SupportRequestResource>(result);

            return Ok(supportRequestResource);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _supportRequestService.DeleteAsync(id);
            var supportRequestResource = _mapper.Map<SupportRequest, SupportRequestResource>(result);

            return Ok(supportRequestResource);
        }
    }
}
