namespace Shoppable.Services.IServices;

public interface IPaymentService
{
    public Task<PaymentVM> SetPaymentVM(int cartid);
}
