using WebmasterAPI.Authentication.Domain.Models;
using WebmasterAPI.Projects.Domain.Models;

namespace WebmasterAPI.Projects.Domain.Services.Communication;

public class DeliverableResponse  {
    
    //Primary Key
    
    public long deliverable_id { get; set; }
    
    //Properties
    
    public string title {get; set;}
    public string descripción { get; set; }
    public string state { get; set; }
    public string file { get; set; }
    
    //Foreign Key
    public long project_id { get; set; }
    public Project Project { get; set; }
    
    public long developer_id { get; set; }
    public Developer Developer { get; set; }
    
}