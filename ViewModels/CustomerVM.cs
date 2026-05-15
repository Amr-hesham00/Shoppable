using System.ComponentModel.DataAnnotations;

namespace Shoppable.ViewModels;

public class CustomerVM
{

    public int Id { get; set; }

    [Required(ErrorMessage = "Full name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 100 characters.")]
    [Display(Name = "Customer Name")]
    public string Name { get; set; }

    [StringLength(250, MinimumLength = 10, ErrorMessage = "Shipping address must be between 10 and 250 characters.")]
    public string? Address { get; set; }

    [StringLength(100, MinimumLength = 2)]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City must contain only letters.")]
    public string City { get; set; }

    [Display(Name = "Phone Number")]
    [RegularExpression(@"^\+?[0-9]{7,15}$", ErrorMessage = "Invalid phone number.")]
    public string Phone { get; set; }


    public List<Order>? orders { get; set; }
}
