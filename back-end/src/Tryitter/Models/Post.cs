using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Post
{
    [Key]
    public int PostId { get; set; }
    public string Content { get; set; } = default!;
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }

}