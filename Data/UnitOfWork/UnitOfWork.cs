using Shoppable.Repositories.IRepositories;

namespace Shoppable.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext context;
    public ICartRepo Cart { get; private set; }
    public IProductRepo Product { get; private set; }
    public ICustomerRepo Customer { get; private set; }
    public ICartItemRepo CartItem { get; private set; }
    public IMerchantRepo Merchant { get; private set; }
    public IOrderItemRepo OrderItem { get; private set; }
    public IOrderRepo Order { get; private set; }
    public IPaymentRepo Payment { get; private set; }

    public UnitOfWork(AppDbContext context, ICartRepo iCartRepo, IProductRepo iProductRepo, ICustomerRepo iCustomerRepo, ICartItemRepo iCartItemRepo, IMerchantRepo iMerchantRepo, IOrderItemRepo iOrderItemRepo, IOrderRepo iOrderRepo, IPaymentRepo iPaymentRepo)
    {
        this.context = context;
        Cart = iCartRepo;
        Product = iProductRepo;
        Customer = iCustomerRepo;
        CartItem = iCartItemRepo;
        Merchant = iMerchantRepo;
        OrderItem = iOrderItemRepo;
        Order = iOrderRepo;
        Payment = iPaymentRepo;
    }

    public async Task Complete()
    {
        await context.SaveChangesAsync();
    }
    public void Dispose()
    {
        context.Dispose();
    }
}
