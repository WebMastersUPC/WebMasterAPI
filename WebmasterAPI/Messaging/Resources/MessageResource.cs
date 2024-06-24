namespace WebmasterAPI.Messaging.Resources
{
    public class MessageResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string AttachmentPath { get; set; } // Incluir la ruta del adjunto
        public DateTime Timestamp { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
    }
}
