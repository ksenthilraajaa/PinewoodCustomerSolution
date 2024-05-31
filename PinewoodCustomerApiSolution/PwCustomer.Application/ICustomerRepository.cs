using PwCustomer.Domain;

namespace PwCustomer.Application;

public interface ICustomerRepository
{
    Task<Customer> GetByIdAsync(int id);

    Task<IEnumerable<Customer>> GetAllAsync();

    Task<int> CreateCustomer(Customer customer);

    Task<int> DeleteCustomer(int id);

    Task<int> UpdateCustomer(Customer customer);
}
