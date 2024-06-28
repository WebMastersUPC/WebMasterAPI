using WebmasterAPI.Authentication.Domain.Models;

namespace WebmasterAPI.ProjectManagement.Domain.Models;

public class Deliverable {
    
    //Primary Key
    
    public long deliverable_id { get; set; }
    
    //Properties
    public string title {get; set;}
    public string description { get; set; }
    
    public string developerDescription { get; set; }
    public string state { get; set; }
    public string file { get; set; }
    
    public DateTime deadlineDateValue { get; set; }
    
    public string deadlineTime { get; set; }
    
    public int orderNumber { get; set; }

    //Foreign Key
    public long projectID { get; set; }
    public Project Project { get; set; }
    
    public long developer_id { get; set; }
    public Developer Developer { get; set; }
    
    public string deadlineDate
    {
        get
        {
            return deadlineDateValue.ToString("yyyy-MM-dd");
        }
    }
    
    public DateTime deadline
    {
        get
        {
            DateTime deadlineDateValue = DateTime.Parse(deadlineDate);
            TimeSpan timeSpan = TimeSpan.Parse(deadlineTime);
            return deadlineDateValue.Date + timeSpan;
        }
    }
    
}