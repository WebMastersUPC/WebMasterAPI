using WebmasterAPI.Models;

namespace WebmasterAPI.Messaging.Domain.Models
{
    public class Enterprise
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public string Industry { get; set; }
        public string TaxId { get; set; }
    }
}
