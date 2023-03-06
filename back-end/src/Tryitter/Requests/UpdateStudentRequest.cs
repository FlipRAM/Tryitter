using System.ComponentModel.DataAnnotations;

public class UpdateStudentRequest 
{
    [MaxLength(50)]
    public string? StudentName { get; set; }
    public Modules? CurrentModule { get; set; }
    public string? Status { get; set; }
    [RegularExpression("^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{6,16}$",
        ErrorMessage = "Your password needs to be stronger")]
    public string? Password { get; set; }

}