using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebmasterAPI.Messaging.Domain.Models;
using WebmasterAPI.Messaging.Domain.Services;
using WebmasterAPI.Messaging.Mapping;
using WebmasterAPI.Messaging.Resources;

namespace WebmasterAPI.Messaging.Interfaces.Rest.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public MessagesController(IMessageService messageService, IMapper mapper, IWebHostEnvironment env)
        {
            _messageService = messageService;
            _mapper = mapper;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] SaveMessageResource resource)
        {
            var userId = User.FindFirst(ClaimTypes.Sid)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            resource.SenderId = int.Parse(userId);

            var message = _mapper.Map<SaveMessageResource, Message>(resource);

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

                message.AttachmentPath = filePath;
            }

            var result = await _messageService.SaveAsync(message);
            var messageResource = _mapper.Map<Message, MessageResource>(result);

            return Ok(messageResource);
        }

        [HttpGet("messages")]
        public async Task<IEnumerable<MessageResource>> GetAllAsync()
        {
            var messages = await _messageService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageResource>>(messages);
            return resources;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var message = await _messageService.GetByIdAsync(id);

            if (message == null)
                return NotFound();

            var messageResource = _mapper.Map<Message, MessageResource>(message);
            return Ok(messageResource);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveMessageResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var message = _mapper.Map<SaveMessageResource, Message>(resource);
            var result = await _messageService.UpdateAsync(id, message);

            if (result == null)
                return NotFound();

            var messageResource = _mapper.Map<Message, MessageResource>(result);
            return Ok(messageResource);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _messageService.DeleteAsync(id);

            if (result == null)
                return NotFound();

            var messageResource = _mapper.Map<Message, MessageResource>(result);
            return Ok(messageResource);
        }

        [HttpGet("receiver/{receiverId}")]

        public async Task<IEnumerable<MessageResource>> GetByReceiverIdAsync(int receiverId)
        {
            var messages = await _messageService.ListByReceiverIdAsync(receiverId);
            var resources = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageResource>>(messages);
            return resources;
        }

        [HttpGet("sender/{senderId}")]

        public async Task<IEnumerable<MessageResource>> GetBySenderIdAsync(int senderId)
        {
            var messages = await _messageService.ListBySenderIdAsync(senderId);
            var resources = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageResource>>(messages);
            return resources;
        }
    }
}
