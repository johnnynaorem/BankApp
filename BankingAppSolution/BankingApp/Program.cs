using BankingApp.Context;
using BankingApp.Interfaces;
using BankingApp.Models;
using BankingApp.Repositories;
using BankingApp.Services;
using Microsoft.EntityFrameworkCore;

namespace BankingApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);





            builder.Services.AddDbContext<BankingContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("MySqlServerConnectionString"));
            });

            builder.Services.AddScoped<IRepository<string, Employee>, EmployeeRepository>();
            builder.Services.AddScoped<IRepository<int, Customer>, CustomerRepository>();


            builder.Services.AddScoped<ICustomerService, CustomerService>();

            // Add services to the container.

            // Register CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()  // Allows all origins (you can restrict this to specific domains)
                          .AllowAnyMethod()  // Allows all HTTP methods (GET, POST, PUT, DELETE, etc.)
                          .AllowAnyHeader(); // Allows any headers (like Authorization)
                });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseCors("AllowAllOrigins");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
