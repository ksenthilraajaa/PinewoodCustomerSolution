using System.ComponentModel.DataAnnotations;

namespace PwCustomer.Domain;

public class Customer : IValidatableObject
{

    [Key]
    [Required(ErrorMessage = "Id is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Id must be a positive number")]
    public int Id { get; set; }

    [Required(ErrorMessage = "First Name is required")]
    [StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last Name is required")]
    [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email address is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number required")]
    public long PhoneNumber { get; set; } = 0;

    [Required(ErrorMessage = "City is required")]
    public string City { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (FirstName.Equals(LastName))
        {
            yield return new ValidationResult(
                "First name and last name cannot be the same",
                new[] { nameof(FirstName), nameof(LastName) });
        }
    }
}
