using Microsoft.EntityFrameworkCore;
using Shoppable.Repositories.Generic;
using Shoppable.Repositories.IRepositories;

namespace Shoppable.Repositories;

public class OrderItemRepo : GenericRepo<OrderItem>, IOrderItemRepo
{
    public OrderItemRepo(AppDbContext context) : base(context)
    { }

    public async Task<List<Order>> FilterItemsByMerchantId(List<Order> orders, int merchantId)
    {
        foreach (var order in orders)
            order.orderItems.RemoveAll(x => x.MerchantId != merchantId);

        return orders;
    }

    public async Task<List<OrderItem>> GetByMerchantId(int merchantid) => await dbset.Where(x => x.MerchantId == merchantid).ToListAsync();
}
