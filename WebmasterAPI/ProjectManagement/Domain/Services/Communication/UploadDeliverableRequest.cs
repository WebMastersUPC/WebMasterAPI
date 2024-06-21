using System.ComponentModel.DataAnnotations;

namespace WebmasterAPI.ProjectManagement.Domain.Services.Communication;

public class UploadDeliverableRequest
{
    [Required] public string developerDescription { get; set; }
    [Required] public string file { get; set; }
}