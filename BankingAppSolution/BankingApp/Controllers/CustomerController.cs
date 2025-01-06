using BankingApp.Exceptions;
using BankingApp.Interfaces;
using BankingApp.Models.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAllOrigins")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService employeeService) {
            _service = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomer() {
            try
            {
                var customers = await _service.GetAllCustomer();
                return Ok(customers);
            }
            catch (EmptyCollectionException ex)
            {
                return Ok();
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                var customer = await _service.GetCustomer(id);
                return Ok(customer);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CreateCustomerDTO updateCustomer)
        {
            try
            {
                var customerId = await _service.UpdateCustomer(id, updateCustomer);
                return Ok(new { message = $"Update Successfull for Id: {customerId}"});

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDTO customer)
        {
            try
            {
                if (ModelState.IsValid) {
                    var customerId = await _service.CreateCustomer(customer);
                    return Ok(new { message = $"Create Successfull for Id: {customerId}" });
                }
                return BadRequest("One or more fields validate error");
                
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
               var deletedCustomerId = await _service.DeleteCustomer(id);
                return Ok(new { message = $"Delete Successfull for Customer Id: {id}" });

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
