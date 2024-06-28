namespace WebmasterAPI.ProjectManagement.Resources;

public class DeliverableResource {
    
    public string description { get; set; }
    public string title {get; set;}
    public string developerDescription { get; set; }
    public string state { get; set; }
    public string file { get; set; }
    
    public DateTime deadline { get; set; }
    
    public int orderNumber { get; set; }
    public long project_id { get; set; }
    public long developer_id { get; set; }
}