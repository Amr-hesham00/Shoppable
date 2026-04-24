using Shoppable.Enum;
using Shoppable.Repositories.Generic;
using Shoppable.Repositories.IRepositories;
using Shoppable.Services.IServices;

namespace Shoppable.Services;

public class OrderService : GenericRepo<Order>, IOrderService
{

    //private readonly IGenericRepo<OrderItem> IOrderItemRepo;
    //private readonly IProductRepo IProductRepo;
    //private readonly IOrderItemRepo IOrderItemRepo;
    private readonly IMerchantRepo IMerchantRepo;
    private readonly IOrderRepo IOrderRepo;
    private readonly ICustomerRepo ICustomerRepo;
    private readonly ICartRepo ICartRepo;

    public OrderService(/*IMerchantRepo IMerchantRepo, IOrderItemRepo IOrderItemRepo, IProductRepo _productrepo,*/ IOrderRepo iOrderRepo, ICustomerRepo iCustomerRepo, ICartRepo iCartRepo, IMerchantRepo iMerchantRepo)
    {
        //this.IMerchantRepo = IMerchantRepo;
        //this.IOrderItemRepo = IOrderItemRepo;
        //IProductRepo = _productrepo;
        IOrderRepo = iOrderRepo;
        ICustomerRepo = iCustomerRepo;
        ICartRepo = iCartRepo;
        IMerchantRepo = iMerchantRepo;
    }

    public async Task<bool> PlaceOrder(PaymentVM VM, string userid)
    {
        Customer? customer = await ICustomerRepo.GetByUserId(userid);
        if (customer == null)
            return false;

        Cart? cart = await ICartRepo.GetWithItemsById(VM.CartId);

        // 1. mapping cart -> order
        var order = new Order
        {
            FullName = $"{VM.FirstName} {VM.LastName}",
            OrderDate = DateOnly.FromDateTime(DateTime.Now),
            TotalAmount = VM.Total,
            Status = OrderStatus.PendingPayment,
            ShippingAddress = VM.FullShippingAddress,
            City = VM.City,
            Country = VM.Country,
            PhoneNumber = VM.PhoneNumber,
            CustomerId = customer.Id,
            orderItems = cart!.cartitems.Select(ci => new OrderItem    // .Select(ci => ...) — loops over each CartItem (ci) and transforms it
            {                                                       // new OrderItem { ... } — creates a new OrderItem for each CartItem, copying the
                ProductId = ci.ProductId,
                MerchantId = ci.product.MerchantId,
                Quantity = ci.Quantity,
                Color = ci.Color,
                Size = ci.Size,
                PriceAtPurchase = ci.product.Price
            }).ToList()
        };


        // 2. Persist the order
        await IOrderRepo.CreateAsync(order);
        await IOrderRepo.SaveAsync();

        // 3. add to customer orders
        customer.orders.Add(order);
        await ICustomerRepo.SaveAsync();

        // 4. clear the cart after successful order
        ICartRepo.Delete(cart);
        await ICartRepo.SaveAsync();

        return true;
    }

    public async Task SaveDeleteAsync(int id)
    {
        Order? order = await IOrderRepo.GetById(id);
        IOrderRepo.Delete(order!);
        await IOrderRepo.SaveAsync();
    }

    public async Task SaveUpdateAsync(Order VM)
    {
        var order = await IOrderRepo.GetById(VM.Id);
        IOrderRepo.Update(order!);
        await IOrderRepo.SaveAsync();

    }
}
