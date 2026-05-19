using System.Linq.Expressions;

namespace Legacy.Shared.Specification;

public abstract class Specification<T>
{
    public bool IsSatisfied(T entity)
    {
        var predicate = ToExpression().Compile();
        return predicate(entity);
    }

    protected abstract Expression<Func<T, bool>> ToExpression();
}
