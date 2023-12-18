using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Backend.Models;

public class Comment
{
    [Key] 
    public int Id { get; set; }
    
    //  користувач, який залишив коментар
    [ForeignKey("CommenterUserId")]
    public int CommenterUserId { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public User CommenterUser { get; set; }
    
    // користувач, якому залишили коментар
    [ForeignKey("CommentedUserId")]
    public int CommentedUserId { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public User CommentedUser { get; set; }

    public string Text { get; set; }

    public DateTime CreatedAt { get; set; }
    public float Rating { get; set; }
    public string Username { get; set; }
    public string AvatarUrl { get; set; } = "/images/default-avatar.jpg";
}