namespace Shoppable.Models;

public class Customer /*: IHasUserId*/
{

    public int Id { get; set; }
    public string Name { get; set; }
    public string? Address { get; set; }
    public string City { get; set; }
    public string Phone { get; set; }


    public string UserID { get; set; }

    public ApplicationUser AppUser { get; set; }
    public Cart cart { get; set; }
    public List<Order> orders { get; set; }

}
