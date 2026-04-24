using Microsoft.EntityFrameworkCore;
using Shoppable.Repositories.Generic;
using Shoppable.Repositories.IRepositories;

namespace Shoppable.Repositories;

public class CartRepo : GenericRepo<Cart>, ICartRepo
{
    public CartRepo(AppDbContext context) : base(context)
    { }

    public Task<Cart?> GetByUserId(string userid)
    {
        return dbset
           .Include(c => c.cartitems).ThenInclude(x => x.product)
           .FirstOrDefaultAsync(c => c.UserId == userid);

        //return dbset.FirstOrDefaultAsync(x => x.UserId == userid);
    }
    public Task<Cart?> GetWithItemsById(int id)
    {
        return dbset
           .Include(c => c.cartitems).ThenInclude(x => x.product)
           .FirstOrDefaultAsync(c => c.Id == id);

        //return dbset.FirstOrDefaultAsync(x => x.id == id);
    }
}