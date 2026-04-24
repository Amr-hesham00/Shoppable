using Microsoft.EntityFrameworkCore;
using Shoppable.Repositories.Generic;
using Shoppable.Repositories.IRepositories;

namespace Shoppable.Repositories;

public class MerchantRepo : GenericRepo<Merchant>, IMerchantRepo
{
    public MerchantRepo(AppDbContext context) : base(context)
    {
    }

    public async Task<Merchant?> GetByUserIdAsync(string? userId) => await dbset.FirstOrDefaultAsync(x => x.AppUserId == userId);
}
