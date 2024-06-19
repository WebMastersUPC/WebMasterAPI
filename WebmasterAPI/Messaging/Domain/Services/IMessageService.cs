using System.Threading.Tasks;
using WebmasterAPI.Messaging.Domain.Models;

namespace WebmasterAPI.Messaging.Domain.Services
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> ListAsync();
        Task<Message> SaveAsync(Message message);
        Task<Message> UpdateAsync(int id, Message message);
        Task<Message> DeleteAsync(int id);
        Task<Message> GetByIdAsync(int id);
        Task<IEnumerable<Message>> ListByReceiverIdAsync(int receiverId); //esto es para obtener los mensajes de un usuario es espefigico
        Task<IEnumerable<Message>> ListBySenderIdAsync(int senderId); //esto es para obtener los mensajes de un usuario es especifico

    }
}
