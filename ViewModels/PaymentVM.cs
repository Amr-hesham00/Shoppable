using System.ComponentModel.DataAnnotations;

namespace Shoppable.ViewModels;

public class PaymentVM
{
    // --- Order info (passed from cart, hidden fields) ---
    public int CustomerId { get; set; }
    public int CartId { get; set; }

    public string PaymentType { get; set; }

    [Display(Name = "Total Amount")]
    public decimal Total { get; set; }
    public List<CartItem> cartitems { get; set; } = new();

    // --- Shipping address ---
    [Required(ErrorMessage = "First name is required.")]
    [Display(Name = "First name")]
    [StringLength(50, MinimumLength = 2)]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters allowed.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required.")]
    [Display(Name = "Last name")]
    [StringLength(50, MinimumLength = 2)]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters allowed.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Street address is required.")]
    [Display(Name = "Street address")]
    [StringLength(200, MinimumLength = 5)]
    public string ShippingAddress { get; set; }

    [Required(ErrorMessage = "City is required.")]
    [Display(Name = "City")]
    [StringLength(100, MinimumLength = 2)]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City must contain only letters.")]
    public string City { get; set; }

    [Required(ErrorMessage = "Country is required.")]
    [Display(Name = "Country")]
    public string Country { get; set; }

    [RegularExpression(@"^[A-Za-z0-9\- ]{3,10}$", ErrorMessage = "Invalid postal code.")]
    [Display(Name = "ZIP / Postal code")]
    public string PostalCode { get; set; }

    [Display(Name = "Phone Number")]
    [Required(ErrorMessage = "Phone Number is required.")]
    [RegularExpression(@"^\+?[0-9]{7,15}$", ErrorMessage = "Invalid phone number.")]
    public string PhoneNumber { get; set; }


    public string FullShippingAddress =>
        $"{ShippingAddress}, {City}{(string.IsNullOrWhiteSpace(PostalCode) ? "" : " " + PostalCode)}";
}
