using System.ComponentModel.DataAnnotations;

public class EmployeeDto
{
    [Required, MaxLength(50)]
    public string FirstName { get; set; } = "";

    [Required, MaxLength(50)]
    public string LastName { get; set; } = "";

    [Required, MaxLength(100)]
    public string Email { get; set; } = "";

    [Required, MaxLength(15)]
    public string Mobile { get; set; } = "";

    [Required]
    public DateTime DateOfBirth { get; set; }

    public IFormFile? Photo { get; set; }

    
    public string FullName => $"{FirstName} {LastName}";
}
