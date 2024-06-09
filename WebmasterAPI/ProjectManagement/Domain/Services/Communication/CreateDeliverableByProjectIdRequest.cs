using System.ComponentModel.DataAnnotations;

namespace WebmasterAPI.ProjectManagement.Domain.Services.Communication;

public class CreateDeliverableByProjectIdRequest
{
    [Required] public string title { get; set; }
    [Required] public string description { get; set; }
    [Required] public string state { get; set; }

    public long developer_id { get; set; }
}