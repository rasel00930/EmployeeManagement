using System.ComponentModel.DataAnnotations;

namespace Management.Models
{
    public class Employee
    {
        public int Id { get; set; }


        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Mobile { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(100)]
        public string ImagePath { get; set; }
        
    }
}
