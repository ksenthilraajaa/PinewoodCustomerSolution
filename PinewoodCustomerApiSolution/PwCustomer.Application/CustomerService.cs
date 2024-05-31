using PwCustomer.Domain;

namespace PwCustomer.Application;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<int> CreateCustomerAsync(Customer customer)
    {
        try
        {
            return await _customerRepository.CreateCustomer(customer);
        }
        catch (NotFoundException ex)
        {
            throw new CustomerNotFoundException(ex.Message);
        }
        catch
        {
            throw;
        }
    }

    public async Task<int> DeleteCustomerAsync(int id)
    {
        try
        {
            return await _customerRepository.DeleteCustomer(id);
        }
        catch (NotFoundException ex)
        {
            throw new CustomerNotFoundException(ex.Message);
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<Customer>> GetAllCustomerAsync()
    {
        return await _customerRepository.GetAllAsync();
    }

    public async Task<Customer> GetCustomerByIdAsync(int id)
    {
        try
        {
            return await _customerRepository.GetByIdAsync(id);
        }
        catch (NotFoundException ex)
        {
            throw new CustomerNotFoundException(ex.Message);
        }
        catch
        {
            throw;
        }
    }

    public async Task<int> UpdateCustomerAsync(Customer customer)
    {
        try
        {
            return await _customerRepository.UpdateCustomer(customer);
        }
        catch (NotFoundException ex)
        {
            throw new CustomerNotFoundException(ex.Message);
        }
        catch
        {
            throw;
        }
    }
}
