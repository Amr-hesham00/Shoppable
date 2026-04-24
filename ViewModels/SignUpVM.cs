using System.ComponentModel.DataAnnotations;

namespace Shoppable.ViewModels;

public class SignUpVM
{
    [Required(ErrorMessage = "*")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "*")]
    [Display(Name = "Full Name")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "*")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "*")]
    [Compare("Password")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "*")]
    [RegularExpression(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        ErrorMessage = "Invalid email format."
    )]
    public string Email { get; set; }

    [Required(ErrorMessage = "*")]
    [DataType(DataType.PhoneNumber)]
    public string Phone { get; set; }

    public string Role { get; set; }


    [Display(Name = "Delivery Address")]
    public string? Address { get; set; }

    [Display(Name = "Remember Me")]
    public bool RememberMe { get; set; }

}
