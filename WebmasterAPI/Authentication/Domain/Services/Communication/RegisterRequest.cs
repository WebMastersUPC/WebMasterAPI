using System.ComponentModel.DataAnnotations;

namespace WebmasterAPI.Authentication.Domain.Services.Communication;

public class RegisterRequest
{
    [Required] public string mail { get; set; }
    [Required] public string password { get; set; }
    [Required] public string names { get; set; }
    [Required] public string lastnames { get; set; }
    public string cellphone { get; set; } = "999 999 999";
    [Required] public char user_type { get; set; }
    
    public string profile_img_url { get; set; } = "https://cdn-icons-png.flaticon.com/512/3237/3237472.png";
}