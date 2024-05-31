using Microsoft.EntityFrameworkCore;
using PwCustomer.Domain;

namespace PwCustomer.Infrastructure;

public interface ICustomerDbContext
{
    DbSet<Customer> Customers { get; set; }
}
