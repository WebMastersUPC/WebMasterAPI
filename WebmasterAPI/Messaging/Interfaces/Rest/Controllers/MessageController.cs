using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebmasterAPI.Messaging.Domain.Models;
using WebmasterAPI.Messaging.Domain.Services;
using WebmasterAPI.Messaging.Mapping;
using WebmasterAPI.Messaging.Resources;

namespace WebmasterAPI.Messaging.Interfaces.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        // Constructor no debe tener un tipo de retorno
        public MessagesController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<MessageResource>> GetAllAsync()
        {
            var messages = await _messageService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageResource>>(messages);
            return resources;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveMessageResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var message = _mapper.Map<SaveMessageResource, Message>(resource);
            var result = await _messageService.SaveAsync(message);

            if (result == null)
                return BadRequest("Error saving the message.");
            //var messages = await _messageService.ListAsync();
            //var resources = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageResource>>(messages);
            var messageResource = _mapper.Map<Message, MessageResource>(result);
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var message = await _messageService.GetByIdAsync(id);

            if (message == null)
                return NotFound();

            var messageResource = _mapper.Map<Message, MessageResource>(message);
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
