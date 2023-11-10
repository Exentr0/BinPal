using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Software
{
    [Key] 
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public byte[] ProfilePicture { get; set; }
    
    public List<SoftwareCategory> SoftwareCategories { get; set; }
}