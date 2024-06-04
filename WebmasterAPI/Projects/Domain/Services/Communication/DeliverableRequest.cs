using System.ComponentModel.DataAnnotations;

namespace WebmasterAPI.Projects.Domain.Services.Communication;

public class DeliverableRequest  {

    public string state { get; set; }
    
    public string title {get; set;}
    public string description { get; set; }
    
}