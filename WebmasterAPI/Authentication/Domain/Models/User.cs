namespace WebmasterAPI.Models;

public class User
{
    // Key
    public int user_id { get; set; }
    
    // Properties
    public string mail { get; set; }
    public string password { get; set; }
    public string user_type { get; set; }
}