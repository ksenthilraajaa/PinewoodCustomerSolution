using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using PwCustomer.Client.ApiServices;
using PwCustomer.Client.Models;

namespace PwCustomer.Client.Controllers;

public class CustomerController : Controller
{
    // Uri apiAddress = new Uri("https://localhost:5001");
    // private readonly HttpClient _httpClient;

    private readonly CustomerApi _customerApi;
    public CustomerController(CustomerApi customerApi)
    {
        _customerApi = customerApi;
        // _httpClient = new HttpClient();
        // _httpClient.BaseAddress = apiAddress;
    }

    [HttpGet]
    public IActionResult All()
    {
        // List<Customer>? customers = new List<Customer>();
        // var response = _httpClient.GetAsync("api/Customer/all");
        // var result = response.Result;
        // if (result.IsSuccessStatusCode)
        // {
        //     var data = result.Content.ReadAsStringAsync().Result;
        //     customers = JsonSerializer.Deserialize<List<Customer>>(data);
        // }

        var customerlist = _customerApi.GetAllCustomers();
        return View(customerlist);
    }
}
