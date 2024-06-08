using WebmasterAPI.Authentication.Domain.Models;

namespace WebmasterAPI.Projects.Domain.Models;

public class Deliverable {
    
    //Primary Key
    
    public long deliverable_id { get; set; }
    
    //Properties
    public string title {get; set;}
    public string description { get; set; }
    
    public string state { get; set; }
    public string file { get; set; }
    
    //Foreign Key
    public long projectID { get; set; }
    public Project Project { get; set; }
    
    public long developer_id { get; set; }
    public Developer Developer { get; set; }
    
}