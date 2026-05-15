using Microsoft.EntityFrameworkCore;
using Shoppable.Specifications.Parents;

namespace Shoppable.Repositories.Generic;

public class GenericRepo<T> : IGenericRepo<T> where T : class //, IHasUserId, IHasMerchantId
{
    // context and dbSet = protected =>  So child repositories can use them.
    protected AppDbContext context;
    protected DbSet<T> dbset; // Dbset<Product>
    public GenericRepo(AppDbContext context)
    {
        this.context = context;
        dbset = context.Set<T>(); // give me: DbSet<Products> (for ex)
    }

    public GenericRepo()
    {
    }

    public async Task CreateAsync(T entity)
    {
        await dbset.AddAsync(entity);
    }
    public void Update(T entity)
    {
        dbset.Update(entity);
    }
    public void Delete(T entity)
    {
        dbset.Remove(entity);
    }
    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }
    public async Task<List<T>> GetAllAsync(T entity)
    {
        return await dbset.ToListAsync();
    }

    public async Task<T?> GetById(int id)
    {
        return await dbset.FindAsync(id); // return entity or null
    }

    public List<T> GetBySpecification(
        ISpecification<T> specification)
    {
        return dbset.Where(specification.Criteria)
            .ToList();
    }
}
