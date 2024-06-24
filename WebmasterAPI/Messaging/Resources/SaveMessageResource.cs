namespace WebmasterAPI.Messaging.Resources
{
    public class SaveMessageResource
    {
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public IFormFile Attachment { get; set; } 
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
    }
}
