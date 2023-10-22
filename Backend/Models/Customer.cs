using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Customer
{
    // Якщо тут десь терба додати FK, то додайте 
    [Key]
    public int Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string First_Name { get; set; }
    public string Last_Name { get; set; }
    public DateTime Registered_at  { get; set; }
    public string Password_hash { get; set; }
    public string Location { get; set; }
    public string Phone_number { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Postal_code { get; set; }
    public byte[] Logo { get; set; }
}