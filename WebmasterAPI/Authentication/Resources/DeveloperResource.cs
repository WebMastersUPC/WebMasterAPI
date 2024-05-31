namespace WebmasterAPI.Authentication.Resources;

public class DeveloperResource
{
    public string mail { get; set; }
    public string password { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string user_type { get; set; } = "D";
    public string description { get; set; } = "No description provided.";
    public string country { get; set; } = "No country provided.";
    public string phone { get; set; } = "999 999 999";
    public int completed_projects { get; set; } = 0;
    public string specialties { get; set; } = "No specialties provided.";
    public string profile_img_url { get; set; } = "https://cdn-icons-png.flaticon.com/512/3237/3237472.png";
    public int user_id { get; set; }
}