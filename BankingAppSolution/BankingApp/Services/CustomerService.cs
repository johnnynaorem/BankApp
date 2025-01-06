using BankingApp.Interfaces;
using BankingApp.Models;
using BankingApp.Models.DTOs;

namespace BankingApp.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<int, Customer> _customerRepo;

        public CustomerService(IRepository<int, Customer> customerRepository)
        {
            _customerRepo = customerRepository;
        }

        private string GenerateAccountNumber()
        {
            Random random = new Random();
            string accountNumber = "";

            for (int i = 0; i < 14; i++)
            {
                accountNumber += random.Next(0, 10).ToString();
            }

            return accountNumber;
        }
        public async Task<int> CreateCustomer(CreateCustomerDTO customer)
        {
            try
            {
                var accountNumber = GenerateAccountNumber();
                var newCustomer = new Customer
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    Address = customer.Address,
                    City = customer.City,
                    DateOfBirth = customer.DateOfBirth,
                    AccountNumber = accountNumber,
                };

                var addCustomer = await _customerRepo.Create(newCustomer);
                return addCustomer.CustomerId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<int> DeleteCustomer(int customerId)
        {
            var deletedCustomer = await _customerRepo.Delete(customerId);
            return deletedCustomer.CustomerId;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomer()
        {
            var customers = await _customerRepo.GetAll();
            return customers;
        }

        public async Task<Customer> GetCustomer(int customerId)
        {
            var customer = await _customerRepo.Get(customerId);
            if (customer == null)
            {
                throw new InvalidOperationException($"Customer with Id: {customerId} is not Found..");
            }
            return customer;
        }

        public async Task<int> UpdateCustomer(int customerId, CreateCustomerDTO updateCustomer)
        {
            try
            {
                var newUpdateCustomer = new Customer
                {
                    FirstName = updateCustomer.FirstName,
                    LastName = updateCustomer.LastName,
                    Email = updateCustomer.Email,
                    Phone = updateCustomer.Phone,
                    Address = updateCustomer.Address,
                    City = updateCustomer.City,
                };
                var updatedCustomer = await _customerRepo.Update(customerId, newUpdateCustomer);
                return updatedCustomer.CustomerId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    } 
}
