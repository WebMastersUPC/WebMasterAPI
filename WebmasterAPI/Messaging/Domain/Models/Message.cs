
using WebmasterAPI.Models;

namespace WebmasterAPI.Messaging.Domain.Models
{
    public class Message
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; } // Añadir campo Subject
        public string Content { get; set; }
        public string AttachmentPath { get; set; } // Añadir campo AttachmentPath
        public DateTime Timestamp { get; set; }
        public long SenderId { get; set; }
        public long ReceiverId { get; set; }
        
        
        public User Sender { get; set; }
        public User Receiver { get; set; }
    }
}
