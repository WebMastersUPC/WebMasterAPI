using System.ComponentModel.DataAnnotations;

namespace WebmasterAPI.ProjectManagement.Domain.Services.Communication;

public class CreateDeliverableByProjectIdRequest
{
    [Required] public string title { get; set; }
    [Required] public string description { get; set; }
    [Required] public DateTime deadline { get; set; }

}