namespace WebmasterAPI.Models;

public class User
{
    public int user_id { get; set; }
    public string mail { get; set; }
    public string password { get; set; }
    public string names { get; set; }
    public string lastnames { get; set; }
    public string cellphone { get; set; }
    public char user_type { get; set; }
    public string profile_img_url { get; set; }
}