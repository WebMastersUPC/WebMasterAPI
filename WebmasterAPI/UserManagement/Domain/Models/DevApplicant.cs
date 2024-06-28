namespace WebmasterAPI.UserManagement.Domain.Models;

public class DevApplicant
{
    // Key
    public long developer_id { get; set; }
    
    // Properties
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string description { get; set; }
    public string profile_img_url { get; set; }
}