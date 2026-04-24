using Shoppable.Repositories.Generic;

namespace Shoppable.Repositories.IRepositories;

public interface ICartRepo : IGenericRepo<Cart>
{
    public Task<Cart?> GetByUserId(string userid);
    public Task<Cart?> GetWithItemsById(int id);

}
