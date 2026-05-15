using Shoppable.Repositories.Generic;

namespace Shoppable.Repositories.IRepositories;

public interface ICustomerRepo : IGenericRepo<Customer>
{
    public Task<Customer?> GetByUserId(string userid);
    public Task<List<Customer>?> GetByMerchantId(int merchantId);

    public Task<Customer> GetById_With_Orders(int id);

}
