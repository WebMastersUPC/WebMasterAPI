using System.ComponentModel.DataAnnotations;

namespace WebmasterAPI.Authentication.Domain.Services.Communication;

public class RegisterRequest
{
    [Required] public string mail { get; set; }
    [Required] public string password { get; set; }
    [Required] public string user_type { get; set; } // "D" or "E"
}