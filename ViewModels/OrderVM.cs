using Shoppable.Enum;
using System.ComponentModel.DataAnnotations;

namespace Shoppable.ViewModels;


public class OrderVM
{
    public int Id { get; set; }
    public int CustomerId { get; set; }

    [Required(ErrorMessage = "Full name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 100 characters.")]
    [Display(Name = "Full Name")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Order date is required.")]
    [Display(Name = "Order Date")]
    public DateOnly OrderDate { get; set; }

    [Required(ErrorMessage = "Total amount is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Total amount must be greater than zero.")]
    [Display(Name = "Total Amount")]
    public decimal TotalAmount { get; set; }

    [Required(ErrorMessage = "Order status is required.")]
    [EnumDataType(typeof(OrderStatus), ErrorMessage = "Invalid order status.")]
    [Display(Name = "Order Status")]
    public OrderStatus Status { get; set; }

    [Required(ErrorMessage = "Shipping address is required.")]
    [StringLength(250, MinimumLength = 10, ErrorMessage = "Shipping address must be between 10 and 250 characters.")]
    [Display(Name = "Shipping Address")]
    public string ShippingAddress { get; set; }

    [Required(ErrorMessage = "Phone number is required.")]
    [Phone(ErrorMessage = "Invalid phone number format.")]
    [StringLength(15, MinimumLength = 7, ErrorMessage = "Phone number must be between 7 and 15 characters.")]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; }

}