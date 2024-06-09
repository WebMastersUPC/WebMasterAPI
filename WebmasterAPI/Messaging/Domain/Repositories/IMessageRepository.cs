using WebmasterAPI.Messaging.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebmasterAPI.Messaging.Domain.Repositories
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> ListAsync();
        Task AddAsync(Message message);
        Task<Message> FindByIdAsync(int id);
        Task<IEnumerable<Message>> ListByReceiverIdAsync(int receiverId); //esto es para obtener los mensajes de un usuario es espefigico
        Task<IEnumerable<Message>> ListBySenderIdAsync(int senderId); //esto es para obtener los mensajes de un usuario es especifico
        void Update(Message message);
        void Remove(Message message);
    }
}
