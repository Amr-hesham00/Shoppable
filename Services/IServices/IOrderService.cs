namespace Shoppable.Services.IServices;

public interface IOrderService
{
    public Task SaveUpdateAsync(OrderVM order);
    public Task<bool> PlaceOrder(PaymentVM VM, string userid);
    public Task SaveDeleteAsync(int id);
    public Task<OrderVM> GetOrderVM(int id);

}
