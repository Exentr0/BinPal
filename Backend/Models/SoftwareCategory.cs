using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class SoftwareCategory
{
    [ForeignKey("SoftwareId")]
    public int SoftwareId { get; set; }
    
    [ForeignKey("CategoryId")]
    public int CategoryId { get; set; }
    
    public Category Category { get; set; }

    public Software Software { get; set; }
}