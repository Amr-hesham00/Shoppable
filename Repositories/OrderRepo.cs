using Microsoft.EntityFrameworkCore;
using Shoppable.Repositories.Generic;
using Shoppable.Repositories.IRepositories;

namespace Shoppable.Repositories;

public class OrderRepo : GenericRepo<Order>, IOrderRepo
{
    public OrderRepo(AppDbContext context) : base(context)
    { }
    public async Task<List<Order>?> OrdersbyMerchantId(int merchantId)
    {
        if (dbset == null) return null; // if orders(dbset) = null; return null;

        return await dbset
            .Where(o => o.orderItems != null && o.orderItems.Any(oi => oi.MerchantId == merchantId)) // return orders which have any orderitem that meet the condition.
            .Include(x => x.orderItems)//.ThenInclude(x => x.product)
            .ToListAsync();
    }

    public async Task<Order?> Order_byId_WithItems(int orderid)
    {
        return await dbset.Include(x => x.orderItems).FirstOrDefaultAsync(x => x.Id == orderid);
    }


    //public async Task<List<Order>?> Ordersby_Merchant_Customer_Ids(int merchantId)
    //{
    //    //if (dbset == null) return null; // if orders(dbset) = null; return null;

    //    return await dbset
    //        .Where(o => o.orderItems != null && o.orderItems.Any(oi => oi.MerchantId == merchantId)) // return orders which have any orderitem that meet the condition.
    //        .ToListAsync();
    //}


}
