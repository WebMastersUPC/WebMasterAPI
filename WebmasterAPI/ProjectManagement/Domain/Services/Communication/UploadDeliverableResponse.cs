namespace WebmasterAPI.ProjectManagement.Domain.Services.Communication;
using System.ComponentModel.DataAnnotations;

public class UploadDeliverableResponse
{
    [Required] public string developerDescription { get; set; }
    [Required] public string file { get; set; }
    
    
}