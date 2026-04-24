namespace Shoppable.Models;

public class OrderItem /*: IHasMerchantId*/
{

    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal PriceAtPurchase { get; set; }
    public string Color { get; set; }
    public string Size { get; set; }

    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int MerchantId { get; set; }
    public Product product { get; set; }
    public Order order { get; set; }
    public Merchant merchant { get; set; }
}
