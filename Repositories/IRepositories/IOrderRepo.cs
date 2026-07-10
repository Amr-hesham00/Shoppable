using Shoppable.Repositories.Generic;

namespace Shoppable.Repositories.IRepositories;

public interface IOrderRepo : IGenericRepo<Order>
{
    public Task<List<Order>?> OrdersbyMerchantId(int merchantId);
    public Task<Order> Order_byId_WithItems(int orderid);
    public Task<List<Order>?> Orders_by_cust_Id_WithItems(int custId);

}
