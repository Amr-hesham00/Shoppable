using Shoppable.Repositories.IRepositories;
namespace Shoppable.Data.UnitOfWork;

public interface IUnitOfWork : IDisposable
{

    ICartRepo Cart { get; }
    IProductRepo Product { get; }
    ICustomerRepo Customer { get; }
    ICartItemRepo CartItem { get; }
    IMerchantRepo Merchant { get; }
    IOrderItemRepo OrderItem { get; }
    IOrderRepo Order { get; }
    IPaymentRepo Payment { get; }

    Task Complete();
}
