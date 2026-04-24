using Shoppable.Enum;

namespace Shoppable.ViewModels;

public class HomeVM
{
    public List<Product>? Products { get; set; }

    public int? Minprice { get; set; }
    public int? Maxprice { get; set; }
    public string? Search { get; set; }
    public Category category { get; set; } // enum

}
