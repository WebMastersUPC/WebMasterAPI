namespace WebmasterAPI.ProjectManagement.Domain.Services.Communication;

public class ProjectDto
{
    public long project_ID { get; set; }
    public string nameProject { get; set; }
    public string descriptionProject { get; set; }
    public List<string> languages { get; set; }
    public List<string> frameworks { get; set; }
    public decimal budget { get; set; }
    public List<string> methodologies { get; set; }
    public List<long> applicants_id { get; set; }
    public long enterprise_id { get; set; }
    public long developer_id { get; set; }
}