using BankingApp.Context;
using BankingApp.Controllers;
using BankingApp.Exceptions;
using BankingApp.Interfaces;
using BankingApp.Models;
using BankingApp.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Expressions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppUnitTesting
{
    internal class CustomerControllerTest
    {
        private DbContextOptions<BankingContext> options;
        private BankingContext context;
        private Mock<ICustomerService> mockCustomerService;
        private CustomerController customerController;


        [SetUp]
        public void SetUp()
        {
            mockCustomerService = new Mock<ICustomerService>();
            customerController = new CustomerController(mockCustomerService.Object);
        }

        [Test]
        public async Task GetCustomer_Success_200_Test()
        {
            var customer = new Customer
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
            mockCustomerService.Setup(s => s.GetCustomer(1))
                .ReturnsAsync(customer);

            var result = await customerController.GetCustomerById(1);

            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }

        [Test]
        public async Task GetCustomer_BadRequest_400_Test()
        {
            mockCustomerService.Setup(s => s.GetCustomer(2))
                .ThrowsAsync(new InvalidOperationException("User Not Found"));

            var result = await customerController.GetCustomerById(2);

            Assert.IsNotNull(result);
            var badRequest = result as ObjectResult;
            Assert.IsNotNull(badRequest);
            Assert.AreEqual(400, badRequest.StatusCode);

        }

        [Test]
        public async Task GetCustomer_InternalServerError_500_Test()
        {
            mockCustomerService.Setup(s => s.GetCustomer(2))
                .ThrowsAsync(new Exception("InternalServer Error"));

            var result = await customerController.GetCustomerById(2);

            Assert.IsNotNull(result);
            var badRequest = result as ObjectResult;
            Assert.IsNotNull(badRequest);
            Assert.AreEqual(500, badRequest.StatusCode);

        }

        [Test]
        public async Task GetAllCustomer_Success_200_Test()
        {
            var customers = new List<Customer>{
                new Customer
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
                }
            };
            mockCustomerService.Setup(s => s.GetAllCustomer())
                .ReturnsAsync(customers);

            var result = await customerController.GetAllCustomer();

            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }

        [Test]
        public async Task GetAllCustomer_EmptyCollectionException_Test()
        {
            var customers = new List<Customer>{};
            mockCustomerService.Setup(s => s.GetAllCustomer())
                .ThrowsAsync(new EmptyCollectionException("Empty Collection"));

            var result = await customerController.GetAllCustomer();

            Assert.IsNotNull(result);
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }

        [Test]
        public async Task GetAllCustomer_InternalServerError_500_Test()
        {
            var customers = new List<Customer> { };
            mockCustomerService.Setup(s => s.GetAllCustomer())
                .ThrowsAsync(new Exception("Server Error"));

            var result = await customerController.GetAllCustomer();

            Assert.IsNotNull(result);
            var badResult = result as ObjectResult;
            Assert.IsNotNull(badResult);
            Assert.AreEqual(500, badResult.StatusCode);

        }

        [Test]
        public async Task Delete_Success_200_Test()
        {
            int customerId = 1;
            mockCustomerService.Setup(s => s.DeleteCustomer(1))
                .ReturnsAsync(customerId);

            var result = await customerController.DeleteCustomer(1);

            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }

        [Test]
        public async Task Delete_BadRequest_400_Test()
        {
            int customerId = 1;
            mockCustomerService.Setup(s => s.DeleteCustomer(1))
                .ThrowsAsync(new InvalidOperationException("User Not Found"));

            var result = await customerController.DeleteCustomer(1);

            Assert.IsNotNull(result);
            var badRequest = result as ObjectResult;
            Assert.IsNotNull(badRequest);
            Assert.AreEqual(400, badRequest.StatusCode);
        }

        [Test]
        public async Task Delete_InternalServerError_500_Test()
        {
            int customerId = 1;
            mockCustomerService.Setup(s => s.DeleteCustomer(1))
                .ThrowsAsync(new Exception("Internal Server Error"));

            var result = await customerController.DeleteCustomer(1);

            Assert.IsNotNull(result);
            var badRequest = result as ObjectResult;
            Assert.IsNotNull(badRequest);
            Assert.AreEqual(500, badRequest.StatusCode);
        }

        [Test]
        public async Task Update_Success_200_Test()
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

            int customerId = 1;
            var updateCustomer = new CreateCustomerDTO
            {
                FirstName = "UpdateTest",
            };

            mockCustomerService.Setup(s => s.UpdateCustomer(1, updateCustomer))
                .ReturnsAsync(customerId);

            var result = await customerController.UpdateCustomer(1, updateCustomer);

            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }

        [Test]
        public async Task Update_BadRequest_400_Test()
        {
            int customerId = 1;
            var updateCustomer = new CreateCustomerDTO
            {
                FirstName = "UpdateTest",
            };

            mockCustomerService.Setup(s => s.UpdateCustomer(1, updateCustomer))
                .ThrowsAsync(new InvalidOperationException("User Not Found"));

            var result = await customerController.UpdateCustomer(1, updateCustomer);

            Assert.IsNotNull(result);
            var badRequest = result as ObjectResult;
            Assert.IsNotNull(badRequest);
            Assert.AreEqual(400, badRequest.StatusCode);

        }

        [Test]
        public async Task Update_InternalServerError_500_Test()
        {
            int customerId = 1;
            var updateCustomer = new CreateCustomerDTO
            {
                FirstName = "UpdateTest",
            };

            mockCustomerService.Setup(s => s.UpdateCustomer(1, updateCustomer))
                .ThrowsAsync(new Exception("Internal Server Error"));

            var result = await customerController.UpdateCustomer(1, updateCustomer);

            Assert.IsNotNull(result);
            var badRequest = result as ObjectResult;
            Assert.IsNotNull(badRequest);
            Assert.AreEqual(500, badRequest.StatusCode);

        }

        [Test]
        public async Task CreateCustomer_Success_200_Test()
        {
            var customerId = 1;
            mockCustomerService.Setup(s => s.CreateCustomer(It.IsAny<CreateCustomerDTO>())).ReturnsAsync(customerId);

            var result = await customerController.CreateCustomer(new CreateCustomerDTO { FirstName = "Johnny", Address = "Bishnupur" });
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task CreateCustomer_BadRequest_400_Test()
        {
            mockCustomerService.Setup(s => s.CreateCustomer(It.IsAny<CreateCustomerDTO>())).ThrowsAsync(new InvalidOperationException("Invalid input"));

            var result = await customerController.CreateCustomer(new CreateCustomerDTO { FirstName = "Johnny", Address = "Bishnupur" });
            Assert.IsNotNull(result);
            var badRequest = result as ObjectResult;
            Assert.IsNotNull(badRequest);
            Assert.AreEqual(400, badRequest.StatusCode);
        }

        [Test]
        public async Task CreateCustomer_InternalServerError_500_Test()
        {
            mockCustomerService.Setup(s => s.CreateCustomer(It.IsAny<CreateCustomerDTO>())).ThrowsAsync(new Exception("Internal Server Error"));

            var result = await customerController.CreateCustomer(new CreateCustomerDTO { FirstName = "Johnny", Address = "Bishnupur" });
            Assert.IsNotNull(result);
            var badRequest = result as ObjectResult;
            Assert.IsNotNull(badRequest);
            Assert.AreEqual(500, badRequest.StatusCode);
        }

        [Test]
        public async Task CreateCustomer_BlankField_BadRequest_400_Test()
        {
            customerController.ModelState.AddModelError("FirstName", "Cannot be blank");

            var result = await customerController.CreateCustomer(new CreateCustomerDTO { FirstName = null, Address = "Bishnupur" });
            Assert.IsNotNull(result);
            var badRequest = result as ObjectResult;
            Assert.IsNotNull(badRequest);
            Assert.AreEqual(400, badRequest.StatusCode);
        }
    }
}
