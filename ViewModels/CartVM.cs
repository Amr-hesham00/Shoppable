namespace Shoppable.ViewModels;

public class CartVM
{
    public int CartId { get; set; }
    public DateOnly CreatedDate { get; set; }
    public int CustomerId { get; set; }
    public string UserId { get; set; }


    public List<CartItem> cartitems { get; set; } = new();

}
