
using WebmasterAPI.Models;

namespace WebmasterAPI.Messaging.Domain.Models
{
    public class Message
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public long SenderId { get; set; }
        public long ReceiverId { get; set; }
        public User Sender { get; set; }
        public User Receiver { get; set; }
    }
}
