using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Backend.Models;

public class Seller
{
    [Key] public int Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string First_Name { get; set; }
    public string Last_Name { get; set; }
    public DateTime Registered_at { get; set; }
    public string Password_hash { get; set; }
    public int Seller_feedback_count { get; set; }
    public byte[] Logo { get; set; }
    public int Seller_rating { get; set; }
}