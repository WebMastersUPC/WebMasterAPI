using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebmasterAPI.Authentication.Domain.Models;

namespace WebmasterAPI.ApiProject.Domain.Models;

public class Project
{   
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ProjectID { get; set; }
    public string NameProject { get; set; }
    public string DescriptionProject { get; set; }
    public List<string> Languages { get; set; }
    public List<string> Frameworks { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Budget { get; set; }
    public List<string> Methodologies { get; set; }
    
    public List<long> Developer_id { get; set; }
    
    public long enterprise_id { get; set; }
    [ForeignKey("enterprise_id")]
    public virtual Enterprise Enterprise { get; set; }
}