using Queries.Ai.Models;
using System.Linq.Dynamic.Core;

namespace Queries.Ai.Utils;

public class QueryExecutor<T>
{

    public dynamic? RunQueries(IQueryable<T> DataCollection, PredicatesDto<T> Predicates)
    {
        if (Predicates == null) throw new ArgumentNullException("There's no predicates to execute");

        IQueryable<T> QueryResult = DataCollection;

        if (Predicates.ReversePredicate == true) QueryResult = QueryResult.Reverse().AsQueryable();

        if (Predicates.WherePredicate != null) QueryResult = QueryResult.AsQueryable().Where(Predicates.WherePredicate).AsQueryable();

        if (Predicates.SelectPredicate != null)
        {
            try
            {
                QueryResult = (IQueryable<T>)QueryResult.Select(Predicates.SelectPredicate).Cast<T>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        if (Predicates.OrderByPredicate != null) QueryResult = QueryResult.OrderBy(Predicates.OrderByPredicate).AsQueryable();
        if (Predicates.OrderByDescendingPredicate != null) QueryResult = QueryResult.OrderByDescending(Predicates.OrderByDescendingPredicate).AsQueryable();

        if (Predicates.ThenByPredicate != null && Predicates.OrderByPredicate != null) QueryResult = QueryResult.OrderBy(Predicates.ThenByPredicate).ThenBy(Predicates.ThenByPredicate).AsQueryable();
        if (Predicates.ThenByDescendingPredicate != null && Predicates.OrderByPredicate != null) QueryResult = QueryResult.OrderBy(Predicates.ThenByDescendingPredicate).ThenByDescending(Predicates.ThenByDescendingPredicate).AsQueryable();

        if (Predicates.SkipPredicate != null) QueryResult = QueryResult.Skip((int)Predicates.SkipPredicate).AsQueryable();

        if (Predicates.TakePredicate != null) QueryResult = QueryResult.Take((int)Predicates.TakePredicate).AsQueryable();

        if (Predicates.DistinctPredicate == true) QueryResult = QueryResult.Distinct().AsQueryable();

        if (Predicates.AveragePredicate != null)
        {
            return QueryResult.Average(Predicates.AveragePredicate);
        }

        return QueryResult;
    }
};