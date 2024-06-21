using WebmasterAPI.Authentication.Domain.Models;
using WebmasterAPI.ProjectManagement.Domain.Models;

namespace WebmasterAPI.ProjectManagement.Domain.Services.Communication;

public class DeliverableResponse  {
    
    //Primary Key
    
    public long deliverable_id { get; set; }
    
    //Properties
    
    public string title {get; set;}
    public string description { get; set; }
    public string state { get; set; }
    public DateTime deadline { get; set; }
    
    //Foreign Key
    public string nameProject { get; set; }
    public long projectID { get; set; }
    public String firstName { get; set; }
    public long developer_id { get; set; }
    
}