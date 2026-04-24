using Microsoft.EntityFrameworkCore;
using Shoppable.Repositories.Generic;
using Shoppable.Repositories.IRepositories;

namespace Shoppable.Repositories;

public class CustomerRepo : GenericRepo<Customer>, ICustomerRepo
{
    public CustomerRepo(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Customer>?> GetByMerchantId(int merchantId)
    {
        if (dbset == null) return null; // Or throw a descriptive error

        return await dbset
        .Where(c => c.orders != null && c.orders.Any(o => o.orderItems.Any(oi => oi.MerchantId == merchantId)))
        .ToListAsync();
    }

    public async Task<Customer?> GetByUserId(string userid) => await dbset.FirstOrDefaultAsync(x => x.UserID == userid);

}
