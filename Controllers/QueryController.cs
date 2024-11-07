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
    }
}
