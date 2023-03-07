using System.ComponentModel.DataAnnotations;
public class PostRequest
{
    [MinLength(1)]
    public string Content { get; set; } = default!;
    public int UserId { get; set; }
}