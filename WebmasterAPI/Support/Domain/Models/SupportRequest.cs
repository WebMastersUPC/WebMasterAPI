using WebmasterAPI.Models;

namespace WebmasterAPI.Support.Domain.Models
{
    public class SupportRequest
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Status { get; set; } // Por ejemplo: "Open", "In Progress", "Closed"
        public string AttachmentPath { get; set; } // Nueva propiedad para el archivo adjunto

        public User User { get; set; } // Navegación a User

    }
}
