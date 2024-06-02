using WebmasterAPI.Models;

namespace WebmasterAPI.Authentication.Domain.Services.Communication;

public class DeveloperResponse
{
    // Key
    public long developer_id { get; set; }
    
    // Properties
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string description { get; set; }
    public string country { get; set; }
    public string phone { get; set; }
    public int completed_projects { get; set; }
    public string specialties { get; set; }
    public string profile_img_url { get; set; }
    
    // Foreign Key    
    public long user_id { get; set; }
    public User User { get; set; }
}