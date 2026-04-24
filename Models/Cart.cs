namespace Shoppable.Models;

public class Cart
{

    public int Id { get; set; }
    public DateOnly CreatedDate { get; set; }

    public int CustomerId { get; set; }
    public string UserId { get; set; }

    public ApplicationUser user { get; set; }
    public Customer customer { get; set; }

    public List<CartItem> cartitems { get; set; } = new();

}
