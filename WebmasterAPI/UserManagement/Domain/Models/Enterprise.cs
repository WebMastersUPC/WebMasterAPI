using WebmasterAPI.Models;

namespace WebmasterAPI.Authentication.Domain.Models;

public class Enterprise
{
    // Key
    public long enterprise_id { get; set; }
    
    // Properties
    public string enterprise_name { get; set; }
    public string description { get; set; }
    public string country { get; set; }
    public string RUC { get; set; }
    public string phone { get; set; }
    public string website { get; set; }
    public string profile_img_url { get; set; }
    public string sector { get; set; }
    
    // Foreign Key
    public long user_id { get; set; }
    public User User { get; set; }
}