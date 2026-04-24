namespace Shoppable.Models;

public class Merchant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly CreatedDate { get; set; }
    public bool IsDeleted { get; set; }

    public string AppUserId { get; set; }
    public ApplicationUser user { get; set; }

    public List<Product> products { get; set; }
    public List<OrderItem> orderitems { get; set; }

}
