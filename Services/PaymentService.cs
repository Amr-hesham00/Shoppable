using Shoppable.Data.UnitOfWork;
using Shoppable.Repositories.IRepositories;
using Shoppable.Services.IServices;

namespace Shoppable.Services;

public class PaymentService : IPaymentService
{
    private readonly IUnitOfWork unitOfWork;

    //IPaymentRepo IPaymentRepo;
    //ICartRepo ICartRepo;
    //    ICartItemRepo ICartItemRepo;
    //    IMerchantRepo IMerchantRepo;
    //    IProductRepo IProductRepo;
    //    IOrderItemRepo IOrderItemRepo;
    //    IOrderRepo IOrderRepo;
    //    ICustomerRepo ICustomerRepo;
    //    UserManager<ApplicationUser> userManager;
    public PaymentService(IPaymentRepo iPaymentRepo, ICartRepo iCartRepo, IUnitOfWork unitOfWork)
    {
        //IPaymentRepo = iPaymentRepo;
        //ICartRepo = iCartRepo;
        this.unitOfWork = unitOfWork;
    }

    public async Task<PaymentVM> SetPaymentVM(int cartid)
    {
        Cart? cart = await unitOfWork.Cart.GetWithItemsById(cartid);
        PaymentVM vm = new()
        {
            cartitems = cart.cartitems,
            CustomerId = cart.CustomerId,
            CartId = cartid
        };

        return vm;
    }
}
