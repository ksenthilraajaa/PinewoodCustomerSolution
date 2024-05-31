using Microsoft.EntityFrameworkCore;
using PwCustomer.Domain;

namespace PwCustomer.Application;

public interface ICustomerDbContext
{
    DbSet<Customer> Customers { get; set; }
}
