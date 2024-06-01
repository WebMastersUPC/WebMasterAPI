namespace WebmasterAPI.Models;

public class User
{
    // Key
    public long user_id { get; set; }
    
    // Properties
    public string mail { get; set; }
    public string password { get; set; }
    public string user_type { get; set; }
}