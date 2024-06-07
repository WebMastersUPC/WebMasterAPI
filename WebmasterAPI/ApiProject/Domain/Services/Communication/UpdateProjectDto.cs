namespace WebmasterAPI.ApiProject.Domain.Services.Communication;

public class UpdateProjectDto
{
    public int projectID { get; set; }
    public string nameProject { get; set; }
    public string descriptionProject { get; set; }
    public List<string> languages { get; set; }
    public List<string> frameworks { get; set; }
    public decimal budget { get; set; }
    public List<string> methodologies { get; set; }
    public List<long> developer_id { get; set; }
    public long enterprise_id { get; set; }
}