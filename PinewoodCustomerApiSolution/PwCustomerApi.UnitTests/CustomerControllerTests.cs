using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PwCustomer.Api.Controllers;
using PwCustomer.Application;
using PwCustomer.Domain;

namespace PwCustomerApi.UnitTests;

public class CustomerControllerTests
{
    private readonly Mock<ICustomerService> _customerMockService;

    private readonly Mock<ILogger<CustomerController>> _loggerMockService;

    private CustomerController _customerController;

    public CustomerControllerTests()
    {
        _customerMockService = new Mock<ICustomerService>();
        _loggerMockService = new Mock<ILogger<CustomerController>>();
        _customerController = new CustomerController(_customerMockService.Object, _loggerMockService.Object);
    }


    [Fact]
    public async Task Get_AllCustomers_Test()
    {
        //Arrange
        _customerMockService.Setup(service => service.GetAllCustomerAsync())
                            .ReturnsAsync(GetAllTestCustomers());


        //Act
        var response = await _customerController.GetAllCustomersAsync();


        //Assert
        var okResult = Assert.IsType<OkObjectResult>(response.Result);
        var returnValue = Assert.IsType<List<Customer>>(okResult.Value);
        Assert.NotNull(okResult);
        Assert.NotNull(returnValue);
        Assert.Single(returnValue);
    }

    [Fact]
    public async Task Get_AllCustomers_NotFound_Test()
    {
        //Arrange
        var notFoundExceptionMessage = "No Customers Found";
        _customerMockService.Setup(service => service.GetAllCustomerAsync())
                                                     .ThrowsAsync(new CustomerNotFoundException(notFoundExceptionMessage));


        //Act
        var response = await _customerController.GetAllCustomersAsync();
        var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(response.Result);

        //Assert
        Assert.Equal(404, notFoundObjectResult.StatusCode);
        Assert.Equal(notFoundExceptionMessage, notFoundObjectResult.Value);
    }


    [Fact]
    public async Task Get_AllCustomers_Exception_Test()
    {
        //Arrange
        var exceptionMessage = "Unknown Exception";
        _customerMockService.Setup(service => service.GetAllCustomerAsync())
                                                     .ThrowsAsync(new Exception(exceptionMessage));


        //Act
        var response = await _customerController.GetAllCustomersAsync();
        var exceptionObjectResult = Assert.IsType<ObjectResult>(response.Result);

        //Assert
        Assert.Equal(500, exceptionObjectResult.StatusCode);
        Assert.Equal(exceptionMessage, exceptionObjectResult.Value);
    }

    private IEnumerable<Customer> GetAllTestCustomers()
    {
        return new List<Customer>
        {
             new Customer { Id = 1, FirstName = "John", LastName = "Walter", City = "London", Email = "John.Walter@hotmail.com", PhoneNumber = 07398762341 }
        };
    }
}