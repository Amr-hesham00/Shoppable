using Shoppable.Repositories.Generic;

namespace Shoppable.Repositories.IRepositories;

public interface IMerchantRepo : IGenericRepo<Merchant>
{
    public Task<Merchant?> GetByUserIdAsync(string? userId);
}
