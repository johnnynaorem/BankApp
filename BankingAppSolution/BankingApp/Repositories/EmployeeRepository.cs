using BankingApp.Context;
using BankingApp.Exceptions;
using BankingApp.Interfaces;
using BankingApp.Models;

namespace BankingApp.Repositories
{
    public class EmployeeRepository : IRepository<string, Employee>
    {
        private readonly BankingContext _context;

        public EmployeeRepository(BankingContext context) { 
            _context = context;
        }
        public async Task<Employee> Create(Employee entity)
        {
            try
            {
                var user = _context.Employees.FirstOrDefault(x => x.EmployeeEmail == entity.EmployeeEmail);
                if (user == null) {
                    await _context.Employees.AddAsync(entity);
                    await _context.SaveChangesAsync();
                    return entity;
                }
                else
                {
                    throw new InvalidOperationException("User Already Exists");
                }
            }
            catch (Exception) {
                throw new CouldNotAddExecption("User Add Failed");
            }
        }

        public async Task<Employee> Delete(string key)
        {
            var user = await Get(key);
            if (user == null) {
                throw new InvalidOperationException("User does not exist");
            }
            _context.Employees.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<Employee> Get(string key)
        {
            var user = _context.Employees.FirstOrDefault(u => u.EmployeeEmail == key);
            return user;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            var users = _context.Employees.ToList();
            if (users.Any()) { 
                return users;
            }
            throw new EmptyCollectionException("Users Collection Empty");
        }

        public async Task<Employee> Update(string key, Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}
