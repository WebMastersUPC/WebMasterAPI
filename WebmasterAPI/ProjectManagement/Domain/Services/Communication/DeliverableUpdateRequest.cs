namespace WebmasterAPI.ProjectManagement.Domain.Services.Communication;

public class DeliverableUpdateRequest {
    
    public string title {get; set;}
    public string description { get; set; }
    public DateTime deadlineDateValue { get; set; }
    public string deadlineTime { get; set; }
    
}