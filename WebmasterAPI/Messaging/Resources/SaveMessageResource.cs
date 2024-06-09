namespace WebmasterAPI.Messaging.Resources
{
    public class SaveMessageResource
    {
        public string Content { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
    }
}
