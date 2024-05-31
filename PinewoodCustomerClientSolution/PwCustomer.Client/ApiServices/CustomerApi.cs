using System.Text;
using System.Text.Json;
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
        try
        {
            var response = await _httpClient.GetFromJsonAsync<Customer>($"api/customer/{id}");
            if (response == null)
            {
                throw new Exception($"Customer {id} not found");
            }
            return response;
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<Customer>> GetAllCustomerAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<Customer>>($"api/customer/all");
            if (response == null)
            {
                throw new Exception("No customers Found");
            }
            return response;
        }
        catch
        {
            throw;
        }
    }

    public async Task AddCustomerAsync(Customer customer)
    {
        try
        {
            string data = JsonSerializer.Serialize(customer);
            StringContent jsonContent = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/customer/add", jsonContent);
            if (!response.IsSuccessStatusCode)
            {
                int statusCode = (int)response.StatusCode;
                string errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Response code {statusCode}: Message: {errorMessage}");
            }
        }
        catch
        {
            throw;
        }
    }

    public async Task EditCustomerAsync(Customer customer)
    {
        try
        {
            string data = JsonSerializer.Serialize(customer);
            StringContent jsonContent = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("api/customer/update", jsonContent);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Response code {response.StatusCode}: Message: {response.Content.ReadAsStringAsync()}");
            }
        }
        catch
        {
            throw;
        }
    }

    public async Task DeleteCustomerAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/customer/delete/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Response code {response.StatusCode}: Message: {response.Content.ReadAsStringAsync()}");
            }
        }
        catch
        {
            throw;
        }
    }

}
