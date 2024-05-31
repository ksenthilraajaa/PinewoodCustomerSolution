using PwCustomer.Application;
using Microsoft.AspNetCore.Mvc;
using PwCustomer.Domain;
using System.Text.Json;

namespace PwCustomer.Api.Controllers;

[ApiController]
[Route("api/customer")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    private readonly ILogger<CustomerController> _logger;

    public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
    {
        _customerService = customerService;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomerAsync(int id)
    {
        try
        {
            _logger.LogInformation($"Invoked GetCustomerAsync method to fetch the customer with ID: {id}");
            return Ok(await _customerService.GetCustomerByIdAsync(id));
        }
        catch (CustomerNotFoundException ex)
        {
            _logger.LogError(ex.Message);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error occurred while fetching the customer with ID: {id}. Error: {ex}");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomersAsync()
    {
        try
        {
            return Ok(await _customerService.GetAllCustomerAsync());
        }
        catch (CustomerNotFoundException ex)
        {
            _logger.LogError(ex.Message);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error occurred while fetching the customers.  Error: {ex}");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("add")]
    public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Received Customer: {JsonSerializer.Serialize(customer)}");
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                foreach (var error in errors)
                {
                    _logger.LogError(error);
                }
                return BadRequest(ModelState);
            }
            _logger.LogInformation($"Received Customer: {JsonSerializer.Serialize(customer)}");

            return Ok(await _customerService.CreateCustomerAsync(customer));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error occurred while creating new customer: {customer} Error: {ex}");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _customerService.UpdateCustomerAsync(customer));
        }
        catch (CustomerNotFoundException ex)
        {
            _logger.LogError(ex.Message);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while updating the existing customer: {customer}, Error: {ex}");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        try
        {
            return Ok(await _customerService.DeleteCustomerAsync(id));
        }
        catch (CustomerNotFoundException ex)
        {
            _logger.LogError(ex.Message);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while delete customer with ID: {id}, Error: {ex}");
            return StatusCode(500, ex.Message);
        }
    }

}
