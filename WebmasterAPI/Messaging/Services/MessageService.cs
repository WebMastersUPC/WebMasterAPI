using WebmasterAPI.Messaging.Domain.Models;
using WebmasterAPI.Messaging.Domain.Repositories;
using WebmasterAPI.Messaging.Domain.Services;
using WebmasterAPI.Shared.Domain.Repositories;

namespace WebmasterAPI.Messaging.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IMessageRepository messageRepository, IUnitOfWork unitOfWork)
        {
            _messageRepository = messageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Message>> ListAsync()
        {
            return await _messageRepository.ListAsync();
        }

        public async Task<Message> SaveAsync(Message message)
        {
            await _messageRepository.AddAsync(message);
            await _unitOfWork.CompleteAsync();

            return message;
        }

        public async Task<Message> UpdateAsync(int id, Message message)
        {
            var existingMessage = await _messageRepository.FindByIdAsync(id);

            if (existingMessage == null)
                return null;

            existingMessage.Content = message.Content;

            _messageRepository.Update(existingMessage);
            await _unitOfWork.CompleteAsync();

            return existingMessage;
        }

        public async Task<Message> DeleteAsync(int id)
        {
            var existingMessage = await _messageRepository.FindByIdAsync(id);

            if (existingMessage == null)
                return null;

            _messageRepository.Remove(existingMessage);
            await _unitOfWork.CompleteAsync();

            return existingMessage;
        }
        public async Task<Message> GetByIdAsync(int id)
        {
            return await _messageRepository.FindByIdAsync(id);
        }
        public async Task<IEnumerable<Message>> ListByReceiverIdAsync(int receiverId)
        {
            return await _messageRepository.ListByReceiverIdAsync(receiverId);
        }
        public async Task<IEnumerable<Message>> ListBySenderIdAsync(int senderId)
        {
            return await _messageRepository.ListBySenderIdAsync(senderId);
        }
    }
}
