using System.ComponentModel.DataAnnotations;

namespace BankingApp.Models.DTOs
{
    public class CreateCustomerDTO
    {
        [Required(ErrorMessage = "Cannot be blank")]
        [MinLength(5, ErrorMessage = "Name must be more than or equal to 5 letters")]
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string? Address { get; set; }
        public string? City { get; set; }

        [Required(ErrorMessage = "Cannot be blank")]
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
