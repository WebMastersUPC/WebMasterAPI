namespace WebmasterAPI.ApiProject.Domain.Services.Communication;

public class UpdateProjectDto
{
    public long ProjectID { get; set; }
    public string NameProject { get; set; }
    public string DescriptionProject { get; set; }
    public List<string> Languages { get; set; }
    public List<string> Frameworks { get; set; }
    public decimal Budget { get; set; }
    public List<string> Methodologies { get; set; }
    public List<long> developer_id { get; set; }
    public long enterprise_id { get; set; }
}