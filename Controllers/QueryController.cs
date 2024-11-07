using Microsoft.AspNetCore.Mvc;
using RestApi.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueryController : ControllerBase
    {
        private readonly CustomerService _customerService;
        private readonly EmployeeService _employeeService;
        private readonly TokenService _tokenService;

        public QueryController(TokenService tokenService, CustomerService customerService, EmployeeService employeeService)
        {
            _tokenService = tokenService;
            _customerService = customerService;
            _employeeService = employeeService;
        }

        [HttpGet("customers")]
        public async Task<IActionResult> GetCustomers()
        {
            var allCustomers = await _customerService.GetCustomersAsync();
            return Ok(allCustomers);
        }
        [HttpGet("employees")]
        public async Task<IActionResult> GetEmployees()
        {
            var allEmployees = await _employeeService.GetEmployeesAsync();
            return Ok(allEmployees);
        }

        [HttpGet("customers/{id}")]
        public async Task<IActionResult> GetCustomerById(string id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
        [HttpGet("employees/{id}")]
        public async Task<IActionResult> GetEmployeeById(string id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        //[HttpPost]
        //public async Task<IActionResult> QueryCustomer([FromBody] CustomerQueryRequest request)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(request.CustomerNumber))
        //        {
        //            return BadRequest(new { message = "Customer number is required." });
        //        }

        //        // Retrieve token to authenticate the request
        //        var token = await _tokenService.GetTokenAsync();
        //        if (token == null)
        //        {
        //            return Unauthorized(new { message = "Token retrieval failed." });
        //        }

        //        // Simulated data retrieval, replace with BC data source
        //        var customer = new Customer
        //        {
        //            CustomerNumber = request.CustomerNumber,
        //            Name = "John Doe",
        //            Email = "john.doe@example.com",
        //            Phone = "123456789",
        //            Address = "123 Main St"
        //        };

        //        if (customer == null)
        //        {
        //            return NotFound(new { message = "Customer not found." });
        //        }

        //        return Ok(customer);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception here
        //        Console.WriteLine($"Error: {ex.Message}");
        //        return StatusCode(500, new { message = "An unexpected error occurred. Try again later." });
        //    }

        //}

        //    [HttpPost]
        //    public async Task<IActionResult> QueryEmployee([FromBody] EmployeeQueryRequest request)
        //    {
        //        if (string.IsNullOrEmpty(request.EmployeeNumber))
        //        {
        //            return BadRequest(new { message = "Employee number is required." });
        //        }

        //        // Retrieve token to authenticate the request
        //        var token = await _tokenService.GetTokenAsync();
        //        if (token == null)
        //        {
        //            return Unauthorized(new { message = "Token retrieval failed." });
        //        }

        //        // Simulated data retrieval, replace with BC data source
        //        var employee = new Employee
        //        {
        //            EmployeeNumber = request.EmployeeNumber,
        //            Name = "Jane Smith",
        //            Department = "IT",
        //            Email = "jane.smith@example.com",
        //            Phone = "987654321"
        //        };

        //        if (employee == null)
        //        {
        //            return NotFound(new { message = "Employee not found." });
        //        }

        //        return Ok(employee);
        //    }
    }
}
