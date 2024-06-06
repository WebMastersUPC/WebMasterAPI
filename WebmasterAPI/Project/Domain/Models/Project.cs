using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebmasterAPI.Authentication.Domain.Models;

namespace WebmasterAPI.Project.Domain.Models;

public class Project
{   
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long projectID { get; set; }
    public string nameProject { get; set; }
    public string descriptionProject { get; set; }
    public List<string> languages { get; set; }
    public List<string> frameworks { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal budget { get; set; }
    public List<string> methodologies { get; set; }
    
    public List<long> developer_id { get; set; }
    
    public long enterprise_id { get; set; }
    [ForeignKey("enterprise_id")]
    public virtual Enterprise Enterprise { get; set; }
}