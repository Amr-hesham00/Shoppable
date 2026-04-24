using Shoppable.Enum;
using System.ComponentModel.DataAnnotations;

namespace Shoppable.ViewModels;

public class CreateProductVM
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public int StockQuantity { get; set; }
    public Category category { get; set; }
    [Display(Name = "Available Colors")]
    public string Colors { get; set; }
    [Display(Name = "Available Sizes")]
    public string Sizes { get; set; }
    public IFormFile? image { get; set; }



}
