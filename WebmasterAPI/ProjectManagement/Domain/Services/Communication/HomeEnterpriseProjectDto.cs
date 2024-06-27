namespace WebmasterAPI.ProjectManagement.Domain.Services.Communication;

public class HomeEnterpriseProjectDto
{
    public long project_ID { get; set; }
    public string nameProject { get; set; }
    public long enterprise_id { get; set; }
    public List<long> applicants_id { get; set; }
    public long developer_id { get; set; }
    public string stateProject { get; set; }
    public decimal projectProgressBar { get; set; }
}