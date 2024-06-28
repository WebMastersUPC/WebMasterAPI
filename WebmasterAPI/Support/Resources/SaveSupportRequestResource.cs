using System.ComponentModel.DataAnnotations;

namespace WebmasterAPI.Support.Resources
{
    public class SaveSupportRequestResource
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string Status { get; set; }
        public IFormFile Attachment { get; set; }
    }
}
