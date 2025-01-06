namespace BankingApp.Models
{
    public enum EmployeeRole
    {
        Branch_Manager, 
        Zone_Manager,
        Account_Manager,
    }
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string EmployeeEmail { get; set; } = string.Empty;
        public string Password { get; set; }

        public EmployeeRole Role { get; set; } = EmployeeRole.Branch_Manager;


    }
}
