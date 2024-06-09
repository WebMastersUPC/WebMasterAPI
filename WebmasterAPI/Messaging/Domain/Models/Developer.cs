using WebmasterAPI.Models;

namespace WebmasterAPI.Messaging.Domain.Models
{
    public class Developer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int Rating { get; set; }
        public string Specialties { get; set; }
    }
}
