using System.ComponentModel.DataAnnotations;

namespace Shoppable.ViewModels;

public class ProductFilterVM
{
    public string? Name { get; set; }

    [Display(Name = "Minimum Price")]
    public decimal? MinPrice { get; set; }

    [Display(Name = "Maximum Price")]
    public decimal? MaxPrice { get; set; }

    [Display(Name = "Qt. greater than")]
    public int? MinStockQuantity { get; set; }

    [Display(Name = "Qt. less than")]
    public int? MaxStockQuantity { get; set; }

    [Display(Name = "Created Before")]
    public DateOnly? CreatedBefore { get; set; }

    [Display(Name = "Created After")]
    public DateOnly? createdAfter { get; set; }

    [Display(Name = "Status")]
    public string? Status { get; set; }

}
