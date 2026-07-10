using System.ComponentModel.DataAnnotations;

namespace Shoppable.ViewModels;

public class AddToCartVM
{
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Please select a color.")]
    public string Color { get; set; }

    [Required(ErrorMessage = "Please select a size.")]
    public string Size { get; set; }

    public int ProductId { get; set; }


}
