using Microsoft.EntityFrameworkCore;
using Shoppable.Repositories.Generic;
using Shoppable.Repositories.IRepositories;

namespace Shoppable.Repositories;

public class ProductRepo : GenericRepo<Product>, IProductRepo
{
    public ProductRepo(AppDbContext context) : base(context) // Call the constructor of the parent class and give it these values cuz parent constructor MUST run first.
    { }

    public async Task<List<Product>> GetByMerchantIdAsync(int Merchantid) => await dbset.Where(x => x.MerchantId == Merchantid).ToListAsync();


}
