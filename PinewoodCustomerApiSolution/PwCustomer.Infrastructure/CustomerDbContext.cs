using Microsoft.EntityFrameworkCore;
using PwCustomer.Domain;

namespace PwCustomer.Infrastructure;

public class CustomerDbContext : DbContext, ICustomerDbContext
{

    public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
    : base(options)
    { }

    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Customer>().HasKey(c => c.Id);
    }

}
