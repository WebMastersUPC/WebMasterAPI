using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using WebmasterAPI.Support.Domain.Models;
using WebmasterAPI.Support.Domain.Services;
using WebmasterAPI.Support.Resources;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
        public async Task<ActionResult<IEnumerable<SupportRequestResource>>> GetAllAsync()
        {
            var supportRequests = await _supportRequestService.ListAsync();
            var resources = _mapper.Map<IEnumerable<SupportRequest>, IEnumerable<SupportRequestResource>>(supportRequests);
            return Ok(resources);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] SaveSupportRequestResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var supportRequest = _mapper.Map<SaveSupportRequestResource, SupportRequest>(resource);

            // Guardar el archivo adjunto si es necesario
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

            // Guardar la solicitud de soporte
            var result = await _supportRequestService.SaveAsync(supportRequest);
            var supportRequestResource = _mapper.Map<SupportRequest, SupportRequestResource>(result);

            return Ok(supportRequestResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromForm] SaveSupportRequestResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var supportRequest = _mapper.Map<SaveSupportRequestResource, SupportRequest>(resource);

            // Guardar el archivo adjunto si es necesario
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
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _supportRequestService.DeleteAsync(id);
            var supportRequestResource = _mapper.Map<SupportRequest, SupportRequestResource>(result);

            return Ok(supportRequestResource);
        }
    }
}
