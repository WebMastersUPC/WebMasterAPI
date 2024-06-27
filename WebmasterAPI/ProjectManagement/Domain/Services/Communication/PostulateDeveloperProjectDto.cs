namespace WebmasterAPI.ProjectManagement.Domain.Services.Communication;

public class PostulateDeveloperProjectDto
{
    public long project_ID { get; set; }
    public string nameProject { get; set; }
    public string descriptionProject { get; set; }
    public List<string> languages { get; set; }
    public List<string> frameworks { get; set; }
    public decimal budget { get; set; }
    public string budgetDescription { get; set; }
    public List<string> methodologies { get; set; }
}