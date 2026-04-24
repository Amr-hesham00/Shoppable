namespace Shoppable.Services.IServices;

public interface IOrderService
{
    public Task SaveUpdateAsync(Order order);
    public Task<bool> PlaceOrder(PaymentVM VM, string userid);
    public Task SaveDeleteAsync(int id);
}
