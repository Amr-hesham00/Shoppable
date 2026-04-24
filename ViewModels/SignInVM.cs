using System.ComponentModel.DataAnnotations;

namespace Shoppable.ViewModels;

public class SignInVM
{


    [Required(ErrorMessage = "*")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "*")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Remember Me")]
    public bool RememberMe { get; set; }



}
