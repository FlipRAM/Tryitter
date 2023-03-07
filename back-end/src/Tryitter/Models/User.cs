using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User 
{
    [Key]
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Status { get; set; }
    public string Password { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Post>? Posts { get; set; }

}