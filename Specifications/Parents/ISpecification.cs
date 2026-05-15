using System.Linq.Expressions;

namespace Shoppable.Specifications.Parents;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
}
