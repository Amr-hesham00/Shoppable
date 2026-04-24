using Shoppable.Enum;

namespace Shoppable.Models;

public class Order
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public DateOnly OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public string ShippingAddress { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string PhoneNumber { get; set; }



    public int CustomerId { get; set; }
    public Customer customer { get; set; }
    public Payment payment { get; set; }
    public List<OrderItem> orderItems { get; set; }
}

