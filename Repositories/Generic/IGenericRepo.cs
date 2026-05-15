using Shoppable.Specifications.Parents;

namespace Shoppable.Repositories.Generic;

public interface IGenericRepo<T> where T : class
{
    public Task CreateAsync(T var);
    public void Update(T var);
    public void Delete(T var);
    public Task SaveAsync();
    public Task<List<T>> GetAllAsync(T var);
    public Task<T?> GetById(int id);

    List<T> GetBySpecification(
     ISpecification<T> specification);
}
