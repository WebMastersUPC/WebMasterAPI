using WebmasterAPI.Authentication.Domain.Models;
namespace WebmasterAPI.ProjectManagement.Domain.Models;

public class Project
{ 
    public long projectID { get; set; }
    public string nameProject { get; set; }
    public string descriptionProject { get; set; }
    public List<string> languages { get; set; }
    public List<string> frameworks { get; set; }
    public decimal budget { get; set; }
    public List<string> methodologies { get; set; }
    
    public List<long> applicants_id { get; set; }
    
    public long enterprise_id { get; set; }
    public virtual Enterprise Enterprise { get; set; }
    
    public long developer_id { get; set; }
    public virtual Developer Developer { get; set; }
}