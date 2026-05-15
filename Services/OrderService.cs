using Shoppable.Data.UnitOfWork;
using Shoppable.Enum;
using Shoppable.Repositories.Generic;
using Shoppable.Repositories.IRepositories;
using Shoppable.Services.IServices;

namespace Shoppable.Services;

public class OrderService : GenericRepo<Order>, IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    //    IGenericRepo<OrderItem> IOrderItemRepo;
    //    IProductRepo IProductRepo;
    //    IOrderItemRepo IOrderItemRepo;
    //IMerchantRepo IMerchantRepo;
    //    IOrderRepo IOrderRepo;
    //    ICustomerRepo ICustomerRepo;
    //    ICartRepo ICartRepo;

    public OrderService(/*IMerchantRepo IMerchantRepo, IOrderItemRepo IOrderItemRepo, IProductRepo _productrepo,*/ IOrderRepo iOrderRepo, ICustomerRepo iCustomerRepo, ICartRepo iCartRepo, IMerchantRepo iMerchantRepo, IUnitOfWork unitOfWork)
    {
        //this.IMerchantRepo = IMerchantRepo;
        //this.IOrderItemRepo = IOrderItemRepo;
        //IProductRepo = _productrepo;
        //IOrderRepo = iOrderRepo;
        //ICustomerRepo = iCustomerRepo;
        //ICartRepo = iCartRepo;
        //IMerchantRepo = iMerchantRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderVM> GetOrderVM(int id)
    {
        var order = await _unitOfWork.Order.GetById(id);

        return new OrderVM
        {
            Id = id,
            CustomerId = order.CustomerId,
            FullName = order.FullName,
            OrderDate = order.OrderDate,
            PhoneNumber = order.PhoneNumber,
            ShippingAddress = order.ShippingAddress,
            Status = order.Status,
            TotalAmount = order.TotalAmount
        };
    }

    public async Task SaveUpdateAsync(OrderVM orderFromRequest)
    {
        Order? OrderFromDB = await _unitOfWork.Order.GetById(orderFromRequest.Id);

        OrderFromDB.Id = orderFromRequest.Id;
        OrderFromDB.CustomerId = orderFromRequest.CustomerId;
        OrderFromDB.FullName = orderFromRequest.FullName;
        OrderFromDB.OrderDate = orderFromRequest.OrderDate;
        OrderFromDB.PhoneNumber = orderFromRequest.PhoneNumber;
        OrderFromDB.ShippingAddress = orderFromRequest.ShippingAddress;
        OrderFromDB.Status = orderFromRequest.Status;
        OrderFromDB.TotalAmount = orderFromRequest.TotalAmount;

        _unitOfWork.Order.Update(OrderFromDB);
        await _unitOfWork.Order.SaveAsync();

    }
    public async Task<bool> PlaceOrder(PaymentVM VM, string userid)
    {
        Customer? customer = await _unitOfWork.Customer.GetByUserId(userid);
        if (customer == null)
            return false;

        Cart? cart = await _unitOfWork.Cart.GetWithItemsById(VM.CartId);

        // 1. mapping cart -> order
        var order = new Order
        {
            FullName = $"{VM.FirstName} {VM.LastName}",
            OrderDate = DateOnly.FromDateTime(DateTime.Now),
            TotalAmount = VM.Total,
            Status = OrderStatus.PendingPayment,
            ShippingAddress = VM.FullShippingAddress,
            //City = VM.City,
            //Country = VM.Country,
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
        await _unitOfWork.Order.CreateAsync(order);
        await _unitOfWork.Order.SaveAsync();

        // 3. add to customer orders
        customer.orders.Add(order);
        await _unitOfWork.Customer.SaveAsync();

        // 4. clear the cart after successful order
        _unitOfWork.Cart.Delete(cart);
        await _unitOfWork.Cart.SaveAsync();

        return true;
    }

    public async Task SaveDeleteAsync(int id)
    {
        Order? order = await _unitOfWork.Order.GetById(id);
        _unitOfWork.Order.Delete(order!);
        await _unitOfWork.Order.SaveAsync();
    }


}
