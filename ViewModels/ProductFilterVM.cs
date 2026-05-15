namespace Shoppable.ViewModels;

public class ProductFilterVM
{
    public string? Name { get; set; }

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public int? MinStockQuantity { get; set; }

    public int? MaxStockQuantity { get; set; }

    public DateOnly? CreatedBefore { get; set; }

    public DateOnly? createdAfter { get; set; }

    public string? Status { get; set; }

}
