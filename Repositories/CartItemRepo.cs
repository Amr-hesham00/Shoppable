using Shoppable.Repositories.Generic;
using Shoppable.Repositories.IRepositories;

namespace Shoppable.Repositories;

public class CartItemRepo : GenericRepo<CartItem>, ICartItemRepo
{
    public CartItemRepo(AppDbContext context) : base(context)
    { }
}
