using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class SoftwareCategory
{
    [Required]
    [ForeignKey("SoftwareId")]
    public int SoftwareId { get; set; }
    
    [Required]
    [ForeignKey("CategoryId")]
    public int CategoryId { get; set; }
    
    [Required]
    public Category Category { get; set; }
    
    [Required]
    public Software Software { get; set; }
}