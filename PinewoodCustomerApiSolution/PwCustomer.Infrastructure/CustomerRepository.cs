using Microsoft.EntityFrameworkCore;
using PwCustomer.Application;
using PwCustomer.Domain;

namespace PwCustomer.Infrastructure;

public class CustomerRepository : ICustomerRepository
{
    private readonly CustomerDbContext _customerDbcontext;

    public CustomerRepository(CustomerDbContext customerDbContext)
    {
        _customerDbcontext = customerDbContext;
    }

    public async Task<int> CreateCustomer(Customer customer)
    {
        try
        {
            _customerDbcontext.Customers.Add(customer);
            return await _customerDbcontext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Unknown error while creating new customer {customer.Id}, {ex}");
        }
    }

    public async Task<int> DeleteCustomer(int id)
    {
        try
        {
            var customer = await GetCustomerById(id);
            _customerDbcontext.Remove(customer);
            return await _customerDbcontext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Unknown error occured while deleting the customer with ID {id}", ex);
        }
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        try
        {
            var customers = await _customerDbcontext.Customers.ToListAsync();
            return customers.Any() ? customers : throw new NotFoundException("No customers found");
        }
        catch (Exception ex)
        {
            throw new Exception($"Unknown error occured while fetching all customers: {ex}");
        }
    }

    public async Task<Customer> GetByIdAsync(int id)
    {
        try
        {
            return await GetCustomerById(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Unknown Error Occured while retreiving the Customer with ID {id}, Error : {ex}");
        }
    }

    public async Task<int> UpdateCustomer(Customer customerToUpdate)
    {
        try
        {
            var customer = await GetCustomerById(customerToUpdate.Id);
            if (customer == null)
            {
                throw new NotFoundException($"Customer with {customerToUpdate.Id} not found");
            }
            _customerDbcontext.Entry(customer).CurrentValues.SetValues(customerToUpdate);
            return await _customerDbcontext.SaveChangesAsync();
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception($"Unknown error occured while updating the customer with ID {customerToUpdate.Id}, {ex}");
        }
    }

    private async Task<Customer> GetCustomerById(int id)
    {
        var customer = await _customerDbcontext.Customers.FindAsync(id);
        return customer ?? throw new NotFoundException($"Customer with ID {id} is not found");
    }

}
