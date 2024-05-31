using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PwCustomer.Client.ApiServices;
using PwCustomer.Client.Models;

namespace PwCustomer.Client.Controllers;

public class CustomerController : Controller
{

    private readonly CustomerApi _customerApi;

    private ILogger<CustomerController> _logger;

    public CustomerController(CustomerApi customerApi, ILogger<CustomerController> logger)
    {
        _customerApi = customerApi;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        try
        {
            var customerlist = await _customerApi.GetAllCustomerAsync();
            return View(customerlist);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while retrieving the customer list. Error: {ex}");
            return View("Error", new ErrorViewModel() { RequestId = "List", ExceptionMessage = ex.Message });
        }
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Add(Customer customer)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            await _customerApi.AddCustomerAsync(customer);
            return RedirectToAction(nameof(List));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while creating the customer {JsonSerializer.Serialize(customer)}. Error: {ex}");
            return View("Error", new ErrorViewModel() { RequestId = "Add", ExceptionMessage = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var customer = await _customerApi.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while retrieving the customer with ID {id}. Error: {ex}");
            return View("Error", new ErrorViewModel() { RequestId = "Add", ExceptionMessage = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Customer customer)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            await _customerApi.EditCustomerAsync(customer);
            return RedirectToAction(nameof(List));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while updating the customer {JsonSerializer.Serialize(customer)}. Error: {ex}");
            return View("Error", new ErrorViewModel() { RequestId = "Add", ExceptionMessage = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {

            var customer = await _customerApi.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while retrieving the customer with ID {id}. Error: {ex}");
            return RedirectToAction(nameof(HomeController.Error), "Home");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Customer customer)
    {
        try
        {
            await _customerApi.DeleteCustomerAsync(customer.Id ?? 0);
            return RedirectToAction(nameof(List));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while updating the customer with ID {customer.Id}. Error: {ex}");
            return RedirectToAction(nameof(HomeController.Error), "Home");
        }
    }
}
