using System.ComponentModel.DataAnnotations;

namespace WebmasterAPI.Authentication.Domain.Services.Communication;

public class AuthenticateRequest
{
    [Required]
    public string Mail { get; set; }
    [Required]
    public string Password { get; set; }
}