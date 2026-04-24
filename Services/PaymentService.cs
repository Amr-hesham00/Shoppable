using Shoppable.Repositories.IRepositories;
using Shoppable.Services.IServices;

namespace Shoppable.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepo IPaymentRepo;
    private readonly ICartRepo ICartRepo;
    //private readonly ICartItemRepo ICartItemRepo;
    //private readonly IMerchantRepo IMerchantRepo;
    //private readonly IProductRepo IProductRepo;
    //private readonly IOrderItemRepo IOrderItemRepo;
    //private readonly IOrderRepo IOrderRepo;
    //private readonly ICustomerRepo ICustomerRepo;
    //private readonly UserManager<ApplicationUser> userManager;
    public PaymentService(IPaymentRepo iPaymentRepo, ICartRepo iCartRepo)
    {
        IPaymentRepo = iPaymentRepo;
        ICartRepo = iCartRepo;
    }

    public async Task<PaymentVM> SetPaymentVM(int cartid)
    {
        Cart? cart = await ICartRepo.GetWithItemsById(cartid);
        PaymentVM vm = new()
        {
            cartitems = cart.cartitems,
            CustomerId = cart.CustomerId,
            CartId = cartid
        };

        return vm;
    }
}
