using BankingApp.Context;
using BankingApp.Interfaces;
using BankingApp.Models;
using BankingApp.Models.DTOs;
using BankingApp.Repositories;
using BankingApp.Services;
using Castle.Core.Resource;
using Moq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppUnitTesting
{
    internal class CustomerServiceTest
    {
        private Mock<IRepository<int, Customer>> _repositoryMock;
        private BankingContext bankingContext;
        private CustomerService customerService;


        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IRepository<int, Customer>>();
            customerService = new CustomerService(_repositoryMock.Object);
        }

        [Test]
        public async Task CreateCustomer_Success()
        {
            var newCustomer = new CreateCustomerDTO
            {
                FirstName = "Test",
                LastName = "TestLastName",
                Address = "TestAddress",
                City = "TestCity",
                DateOfBirth = DateTime.Now,
                Phone = "8787470933",
                Email = "john@gmail.com"
            };

            var customer = new Customer
            {
                CustomerId = 1,
                FirstName = "Test",
                LastName = "TestLastName",
                Address = "TestAddress",
                City = "TestCity",
                DateOfBirth = DateTime.Now,
                Phone = "8787470933",
                Email = "john@gmail.com",
                AccountNumber = "1234567895"
            };

            _repositoryMock.Setup(repo => repo.Create(It.IsAny<Customer>())).ReturnsAsync(customer);

            var customerId = await customerService.CreateCustomer(newCustomer);

            Assert.IsNotNull(customerId);
            Assert.AreEqual(1, customerId);
        }

        [Test]
        public async Task CreateCustomer_Exception_Test()
        {
            var newCustomer = new CreateCustomerDTO
            {
                FirstName = "",
                LastName = "TestLastName",
                Address = "TestAddress",
                City = "TestCity",
                DateOfBirth = DateTime.Now,
                Phone = "8787470933",
                Email = "john@gmail.com"
            };

            var customer = new Customer
            {
                CustomerId = 1,
                FirstName = "Test",
                LastName = "TestLastName",
                Address = "TestAddress",
                City = "TestCity",
                DateOfBirth = DateTime.Now,
                Phone = "8787470933",
                Email = "john@gmail.com",
                AccountNumber = "1234567895"
            };

            _repositoryMock.Setup(repo => repo.Create(It.IsAny<Customer>())).ThrowsAsync(new Exception("Error Occur"));
            var exception = Assert.ThrowsAsync<Exception>(() => customerService.CreateCustomer(newCustomer));

            Assert.IsNotNull(exception);
            Assert.AreEqual("Error Occur", exception.Message);
        }

        [Test]
        public async Task Get_Success()
        {
            var customer = new Customer
            {
                CustomerId = 1,
                FirstName = "Test",
                LastName = "TestLastName",
                Address = "TestAddress",
                City = "TestCity",
                DateOfBirth = DateTime.Now,
                Phone = "8787470933",
                Email = "john@gmail.com",
                AccountNumber = "1234567895"
            };

            _repositoryMock.Setup(repo => repo.Get(1)).ReturnsAsync(customer);

            var result = await customerService.GetCustomer(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.CustomerId);
        }

        [Test]
        public async Task Get_Failed_InvalidOperatorException()
        {
            _repositoryMock.Setup(repo => repo.Get(12)).ReturnsAsync((Customer)null);

            var exception = Assert.ThrowsAsync<InvalidOperationException>(() => customerService.GetCustomer(12));

            Assert.IsNotNull(exception);
            Assert.AreEqual("Customer with Id: 12 is not Found..", exception.Message);
        }

        [Test]
        public async Task GetAll_Success_Test()
        {
            var customers = new List<Customer>
            {
                new Customer
            {
                CustomerId = 1,
                FirstName = "Test",
                LastName = "TestLastName",
                Address = "TestAddress",
                City = "TestCity",
                DateOfBirth = DateTime.Now,
                Phone = "8787470933",
                Email = "john@gmail.com",
                AccountNumber = "1234567895"
            },
                new Customer
            {
                CustomerId = 2,
                FirstName = "Test2",
                LastName = "TestLastName2",
                Address = "TestAddress2",
                City = "TestCity2",
                DateOfBirth = DateTime.Now,
                Phone = "8787470933",
                Email = "john2@gmail.com",
                AccountNumber = "12345627895"
            }
            };

            _repositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(customers);

            var result = await customerService.GetAllCustomer();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }


        [Test]
        public async Task Delete_Success_Test()
        {
            var customer = new Customer
            {
                CustomerId = 1,
                FirstName = "Test",
                LastName = "TestLastName",
                Address = "TestAddress",
                City = "TestCity",
                DateOfBirth = DateTime.Now,
                Phone = "8787470933",
                Email = "john@gmail.com",
                AccountNumber = "1234567895"
            };

            _repositoryMock.Setup(repo => repo.Delete(1)).ReturnsAsync(customer);
            var result = customerService.DeleteCustomer(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Update_Success_Test()
        {
            var customer = new Customer
            {
                CustomerId = 1,
                FirstName = "Test",
                LastName = "TestLastName",
                Address = "TestAddress",
                City = "TestCity",
                DateOfBirth = DateTime.Now,
                Phone = "8787470933",
                Email = "john@gmail.com",
                AccountNumber = "1234567895"
            };


            var updatedcustomer = new Customer
            {
                CustomerId = 1,
                FirstName = "UpdateTest",
                LastName = "TestLastName",
                Address = "TestAddress",
                City = "TestCity",
                DateOfBirth = DateTime.Now,
                Phone = "8787470933",
                Email = "john@gmail.com",
                AccountNumber = "1234567895"
            };

            var updatecustomer = new CreateCustomerDTO
            {
                FirstName = "UpdateTest",
            };

            _repositoryMock.Setup(repo => repo.Update(1, customer)).ReturnsAsync(updatedcustomer);
            var result = customerService.UpdateCustomer(1,updatecustomer);
            Assert.IsNotNull(result);
        }
    }
}
