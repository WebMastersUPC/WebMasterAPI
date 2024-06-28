namespace WebmasterAPI.UserManagement.Domain.Models;

public class EnterpriseProfile
{
    // Key
    public long enterprise_id { get; set; }
    
    // Properties
    public string enterprise_name { get; set; } 
    
    public string profile_img_url { get; set; }
    
    public long user_id { get; set; }
}