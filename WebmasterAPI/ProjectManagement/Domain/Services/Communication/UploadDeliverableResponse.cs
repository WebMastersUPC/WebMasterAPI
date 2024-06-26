namespace WebmasterAPI.ProjectManagement.Domain.Services.Communication;
using System.ComponentModel.DataAnnotations;

public class UploadDeliverableResponse
{
  
    public int orderNumber { get; set; }
    public string developerDescription { get; set; }
    public string file { get; set; }
    
    
}