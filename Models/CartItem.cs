namespace Shoppable.Models;

public class CartItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public string Color { get; set; }
    public string Size { get; set; }

    public int ProductId { get; set; }
    public int CartId { get; set; }
    public Product product { get; set; }
    public Cart cart { get; set; }


}
