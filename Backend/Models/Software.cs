using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Software
{
    [Key] public int Id { get; set; }
    [Required][MaxLength(50)] public string Name { get; set; }
    [Required] public byte[] ProfilePicture { get; set; }
    [Required] public List<SoftwareCategory> SoftwareCategories { get; set; }
}