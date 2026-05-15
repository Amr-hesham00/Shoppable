using Shoppable.Enum;

namespace Shoppable.ViewModels;

public class OrdersFilterVM
{
    //public string? Name { get; set; }

    public int? Id { get; set; }

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public int? MinStockQuantity { get; set; }

    public int? MaxStockQuantity { get; set; }

    public DateOnly? CreatedBefore { get; set; }

    public DateOnly? createdAfter { get; set; }

    public OrderStatus? orderstatus { get; set; }
    public List<Order>? Orders { get; set; } // orders by customer
}