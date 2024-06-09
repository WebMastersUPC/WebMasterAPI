using Microsoft.EntityFrameworkCore;
using WebmasterAPI.Messaging.Domain.Models;
using WebmasterAPI.Messaging.Domain.Repositories;
using WebmasterAPI.Shared.Persistence.Contexts;
using WebmasterAPI.Shared.Persistence.Repositories;

using WebmasterAPI.Authentication.Domain.Models;


namespace WebmasterAPI.Messaging.Persistence
{
    public class MessageRepository : BaseRepository, IMessageRepository
    {
        public MessageRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Message>> ListAsync()
        {
            return await _Context.Messages
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .ToListAsync();
        }

        public async Task AddAsync(Message message)
        {
            await _Context.Messages.AddAsync(message);
        }

        public async Task<Message> FindByIdAsync(int id)
        {
            return await _Context.Messages
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public void Update(Message message)
        {
            _Context.Messages.Update(message);
        }

        public void Remove(Message message)
        {
            _Context.Messages.Remove(message);
        }

        public async Task<IEnumerable<Message>> ListByReceiverIdAsync(int receiverId) 
        {
            return await _Context.Messages
                .Where(m => m.ReceiverId == receiverId)
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .ToListAsync();
        }
        public async Task<IEnumerable<Message>> ListBySenderIdAsync(int senderId) 
        {
            return await _Context.Messages
                .Where(m => m.SenderId == senderId)
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .ToListAsync();
        }
    }
}
