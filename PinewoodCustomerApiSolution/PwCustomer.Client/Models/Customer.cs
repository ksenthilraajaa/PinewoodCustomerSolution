using System.ComponentModel.DataAnnotations;

namespace PwCustomer.Client.Models;

public class Customer
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public long PhoneNumber { get; set; } = 0;

    [Required]
    public string City { get; set; } = string.Empty;
}
