namespace WebmasterAPI.ProjectManagement.Domain.Services.Communication;

public class AvailableProjectDto
{
    public long project_ID { get; set; }
    public string nameProject { get; set; }
    public string descriptionProject { get; set; }
    public long enterprise_id { get; set; }
}