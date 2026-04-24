using Shoppable.Enum;
using System.ComponentModel.DataAnnotations;

namespace Shoppable.Models;

public class Product /*: IHasMerchantId*/
{

    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public int StockQuantity { get; set; }
    public string? ImageUrl { get; set; }
    public DateOnly CreatedDate { get; set; }
    public bool IsDeleted { get; set; } = false;
    public Category category { get; set; }
    [Display(Name = "Available Colors")]
    public List<string> Colors { get; set; } = new();
    [Display(Name = "Available Sizes")]
    public List<string> Sizes { get; set; } = new();



    public int MerchantId { get; set; }
    public Merchant? merchant { get; set; }

    public List<CartItem>? cartitems { get; set; }
    public List<OrderItem>? orderitems { get; set; }

}

