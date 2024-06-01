using System.ComponentModel.DataAnnotations;

namespace WebmasterAPI.Authentication.Domain.Services.Communication;

public class RegisterDeveloperRequest : RegisterRequest
{
    [Required] public string firstName { get; set; }
    [Required] public string lastName { get; set; }
}