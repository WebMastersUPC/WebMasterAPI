namespace WebmasterAPI.Authentication.Resources;

public class EnterpriseResource
{
    public string Mail { get; set; }
    public string Password { get; set; }
    public string EnterpriseName { get; set; }
    public string user_type { get; set; } = "E";
    public string Description { get; set; } = "No description provided.";
    public string Country { get; set; } = "No country provided.";
    public string RUC { get; set; } = "No RUC provided.";
    public string Phone { get; set; } = "No phone provided.";
    public string Website { get; set; } = "No website provided.";
    public string ProfileImgUrl { get; set; } = "https://cdn-icons-png.flaticon.com/512/3237/3237472.png";
    public string Sector { get; set; } = "No sector provided.";
}