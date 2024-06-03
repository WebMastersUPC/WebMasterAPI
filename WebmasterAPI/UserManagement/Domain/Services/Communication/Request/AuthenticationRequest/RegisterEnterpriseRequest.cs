using System.ComponentModel.DataAnnotations;

namespace WebmasterAPI.Authentication.Domain.Services.Communication;

public class RegisterEnterpriseRequest : RegisterRequest
{
    [Required] public string enterprise_name { get; set; }
}
    