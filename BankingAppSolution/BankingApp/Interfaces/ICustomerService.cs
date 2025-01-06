using BankingApp.Models;
using BankingApp.Models.DTOs;

namespace BankingApp.Interfaces
{
    public interface ICustomerService
    {
        Task<int> CreateCustomer(CreateCustomerDTO customer);
        Task<int> UpdateCustomer(int customerId, CreateCustomerDTO updateCustomer);
        Task<int> DeleteCustomer(int customerId);
        Task<Customer> GetCustomer(int customerId);
        Task<IEnumerable<Customer>> GetAllCustomer();
    }
}
