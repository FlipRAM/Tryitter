using System.ComponentModel.DataAnnotations;

public class UserRequest 
{
    [Required]
    [MaxLength(50)]
    public string UserName { get; set; }
    [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$",
        ErrorMessage = "Email must be in correct format")]
    public string Email { get; set; }
    public string Status { get; set; }
    [Required]
    [RegularExpression("^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{6,16}$",
        ErrorMessage = "Your password needs to be stronger")]
    public string Password { get; set; }

}