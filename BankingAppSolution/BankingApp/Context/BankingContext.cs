using BankingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Context
{
    public class BankingContext : DbContext
    {
        public BankingContext(DbContextOptions<BankingContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }

    }

}
