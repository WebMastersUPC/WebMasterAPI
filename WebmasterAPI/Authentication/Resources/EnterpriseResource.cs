namespace WebmasterAPI.Authentication.Resources;

public class EnterpriseResource
{
    public string mail { get; set; }
    public string password { get; set; }
    public string enterprise_name { get; set; }
    public string user_type { get; set; } = "E";
    public string description { get; set; } = "No description provided.";
    public string country { get; set; } = "No country provided.";
    public string RUC { get; set; } = "No RUC provided.";
    public string phone { get; set; } = "999 999 999";
    public string website { get; set; } = "No website provided.";
    public string profile_img_url { get; set; } = "https://cdn-icons-png.flaticon.com/512/3237/3237472.png";
    public string sector { get; set; } = "No sector provided.";
    public long user_id { get; set; }
}