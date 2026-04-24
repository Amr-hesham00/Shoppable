using Shoppable.Repositories.Generic;

namespace Shoppable.Repositories.IRepositories;

public interface IProductRepo : IGenericRepo<Product>  // that 
{
    public Task<List<Product>> GetByMerchantIdAsync(int merchantid);
}
