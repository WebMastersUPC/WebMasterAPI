namespace WebmasterAPI.ProjectManagement.Domain.Services.Communication;

public class DeliverableUpdateRequest {
    
    public string title {get; set;}
    public string description { get; set; }
    public string state { get; set; }
    
    public DateTime deadline { get; set; }
    
}