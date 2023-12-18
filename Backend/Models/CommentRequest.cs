namespace Backend.Models;

public class CommentRequest
{
    public int CommentedUserId { get; set; }
    public string Text { get; set; }
    public float Rating { get; set; }
}