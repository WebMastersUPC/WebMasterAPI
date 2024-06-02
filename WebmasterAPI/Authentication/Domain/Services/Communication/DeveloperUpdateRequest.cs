namespace WebmasterAPI.Authentication.Domain.Services.Communication;

public class DeveloperUpdateRequest
{
    public string description { get; set; }
    public string country { get; set; }
    public string phone { get; set; }
    public int completed_projects { get; set; }
    public string specialties { get; set; }
    public string profile_img_url { get; set; }
}