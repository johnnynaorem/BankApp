using BankingApp.Context;
using BankingApp.Exceptions;
using BankingApp.Interfaces;
using BankingApp.Models;

namespace BankingApp.Repositories
{
    public class CustomerRepository : IRepository<int, Customer>
    {
        private readonly BankingContext _context;

        public CustomerRepository(BankingContext context)
        {
            _context = context;
        }
        public async Task<Customer> Create(Customer entity)
        {
            try
            {
                await _context.Customers.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;

            }
            catch (Exception ex) {
                throw new CouldNotAddExecption("Customer Add Failed");
            }
        }

        public async Task<Customer> Delete(int key)
        {
            var customer = await Get(key);
            if (customer == null)
            {
                throw new InvalidOperationException("User does not exist");
            }
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> Get(int key)
        {

            var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == key);
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            var customers = _context.Customers.ToList();
            if (customers.Any())
            {
                return customers;
            }
            throw new EmptyCollectionException("Customers Collection Empty");
        }

        public async Task<Customer> Update(int key, Customer entity)
        {
            try
            {
                var updatedCustomer = await Get(key);
                if (updatedCustomer != null)
                {
                    if (!string.IsNullOrWhiteSpace(entity.FirstName))
                    {
                        updatedCustomer.FirstName = entity.FirstName;
                    }
                    if (!string.IsNullOrWhiteSpace(entity.LastName))
                    {
                        updatedCustomer.LastName = entity.LastName;
                    }

                    if (!string.IsNullOrWhiteSpace(entity.Email))
                    {
                        updatedCustomer.Email = entity.Email;
                    }

                    if (!string.IsNullOrWhiteSpace(entity.Address))
                    {
                        updatedCustomer.Address = entity.Address;
                    }

                    if (!string.IsNullOrWhiteSpace(entity.Phone))
                    {
                        updatedCustomer.Phone = entity.Phone;
                    }

                    if (!string.IsNullOrWhiteSpace(entity.City))
                    {
                        updatedCustomer.City = entity.City;
                    }

                    await _context.SaveChangesAsync();
                    return updatedCustomer;
                }
                else
                {
                    throw new InvalidOperationException("User doesn't exist");
                }
            }
            catch (Exception ex) {
                throw new Exception($"Failed Customer Update {ex.Message}");
            }
        }
    }
}
