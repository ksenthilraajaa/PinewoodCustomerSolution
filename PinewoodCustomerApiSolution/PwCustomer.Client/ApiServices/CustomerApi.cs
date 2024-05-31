using PwCustomer.Client.Models;

namespace PwCustomer.Client.ApiServices;

public class CustomerApi
{
    private readonly HttpClient _httpClient;

    public CustomerApi(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Customer> GetCustomerByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Customer>($"api/customer/{id}");
    }

    public async Task<IEnumerable<Customer>> GetAllCustomers()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Customer>>($"api/customer/all");
    }

}
