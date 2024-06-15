using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using PwCustomer.Api;
using PwCustomer.Domain;

namespace PwCustomerApi.IntegrationTests;

public class CustomerControllerTests
{

    private readonly HttpClient _httpClient;

    private readonly TestServer _testServer;

    public CustomerControllerTests()
    {

        //Arrange
        _testServer = new TestServer(new WebHostBuilder()
                                                    .UseStartup<Startup>());

        _httpClient = _testServer.CreateClient();
    }

    [Fact]
    public async Task Get_ListOfCustomers_Test()
    {
        //Act
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<Customer>>($"api/customer/all");

        //Assert
        Assert.NotNull(response);
        Assert.NotEmpty(response);
        Assert.Equal(5, response.Count());

    }

    [Theory]
    [InlineData(5)]
    public async Task Get_CustomerById_Test(int id)
    {
        //Act
        var response = await _httpClient.GetFromJsonAsync<Customer>($"api/customer/{id}");

        //Assert
        Customer expectedCustomer = new Customer
        {
            Id = 5,
            FirstName = "Brad",
            LastName = "Pitt",
            City = "Manchester",
            Email = "Brad.Pitt@outlook.com",
            PhoneNumber = 7383462341
        };

        Assert.NotNull(response);
        Assert.Equal(expectedCustomer.GetType(), response.GetType());
        Assert.Equal(expectedCustomer.Id, response.Id);
        Assert.Equal(expectedCustomer.FirstName, response.FirstName);
        Assert.Equal(expectedCustomer.LastName, response.LastName);
        Assert.Equal(expectedCustomer.City, response.City);
        Assert.Equal(expectedCustomer.Email, response.Email);
        Assert.Equal(expectedCustomer.PhoneNumber, response.PhoneNumber);
    }
}