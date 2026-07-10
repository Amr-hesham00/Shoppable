using Shoppable.Enum;

namespace Shoppable.ViewModels;

public class ShopVM
{
    public List<Product>? Products { get; set; }

    public List<Product>? NewArrivals { get; set; }

    public int? Minprice { get; set; }
    public int? Maxprice { get; set; }
    public string? Search { get; set; }
    public Category category { get; set; } = Category.All;

    /// <summary>Filter by exact color name (case-insensitive). Empty = all colors.</summary>
    public string? Color { get; set; }

    /// <summary>Filter by size (e.g. S, M, L, XL). Empty = all sizes.</summary>
    public string? Size { get; set; }

    /// <summary>When true only products with StockQuantity > 0 are returned.</summary>
    public bool InStock { get; set; } = false;
}
