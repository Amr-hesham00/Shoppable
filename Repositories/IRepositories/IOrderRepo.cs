using Shoppable.Repositories.Generic;

namespace Shoppable.Repositories.IRepositories;

public interface IOrderRepo : IGenericRepo<Order>
{
    public Task<List<Order>?> OrdersbyMerchantId(int merchantId);
    public Task<Order> Order_byId_WithItems(int orderid);

}
