namespace BankingApp.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? City { get; set; }
        public DateTime DateOfBirth { get; set; } 
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AccountNumber {  get; set; } = string.Empty;
    }
}
