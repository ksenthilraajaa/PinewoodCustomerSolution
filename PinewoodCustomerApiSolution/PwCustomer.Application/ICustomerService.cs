using PwCustomer.Domain;

namespace PwCustomer.Application;

public interface ICustomerService
{
    Task<Customer> GetCustomerByIdAsync(int id);

    Task<IEnumerable<Customer>> GetAllCustomerAsync();

    Task<int> CreateCustomerAsync(Customer customer);

    Task<int> UpdateCustomerAsync(Customer customer);

    Task<int> DeleteCustomerAsync(int id);
}
