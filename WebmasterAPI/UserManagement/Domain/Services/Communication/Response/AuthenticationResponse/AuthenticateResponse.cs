namespace WebmasterAPI.Authentication.Domain.Services.Communication;

public class AuthenticateResponse
{
    public long user_id { get; set; }
    public string token { get; set; }
    public string user_type { get; set; }
}