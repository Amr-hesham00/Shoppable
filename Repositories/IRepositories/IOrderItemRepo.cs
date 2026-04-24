using Shoppable.Repositories.Generic;

namespace Shoppable.Repositories.IRepositories;

public interface IOrderItemRepo : IGenericRepo<OrderItem>
{
    public Task<List<OrderItem>> GetByMerchantId(int merchantid);
    public Task<List<Order>> FilterItemsByMerchantId(List<Order> Order, int merchantId); // filter items in a orders to give the merchant a list of orders which have just his products

}
