using Queries.Ai.Models;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Queries.Ai.Utils;

public class QueryExecutor<T>
{

    public IQueryable<T> RunQueries(IQueryable<T> DataCollection, PredicatesDto<T> Predicates )
    {
        if (Predicates == null) throw new ArgumentNullException("There's no predicates to execute");

        IQueryable<T> QueryResult = DataCollection;

        if (Predicates.WherePredicate != null) QueryResult = DataCollection.AsQueryable().Where(Predicates.WherePredicate).AsQueryable();
        if (Predicates.SelectPredicate != null)
        {
            try
            {
                QueryResult = (IQueryable<T>)DataCollection.Select(Predicates.SelectPredicate);
            }catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        if (Predicates.OrderByPredicate != null) QueryResult = DataCollection.OrderBy(Predicates.OrderByPredicate).AsQueryable();
        if (Predicates.OrderByDescendingPredicate != null) QueryResult = DataCollection.OrderByDescending(Predicates.OrderByDescendingPredicate).AsQueryable();

        if (Predicates.ThenByPredicate != null && Predicates.OrderByPredicate != null) QueryResult = DataCollection.OrderBy(Predicates.ThenByPredicate).ThenBy(Predicates.ThenByPredicate).AsQueryable();
        if (Predicates.ThenByDescendingPredicate != null && Predicates.OrderByPredicate != null) QueryResult = DataCollection.OrderBy(Predicates.ThenByDescendingPredicate).ThenByDescending(Predicates.ThenByDescendingPredicate).AsQueryable();
        if (Predicates.AveragePredicate != null) 
        {
            //TODO 
            //decimal Average = DataCollection.Average(Predicates.AveragePredicate);
            //return Average;
            //break;
            return QueryResult;
        }

        if (Predicates.SkipPredicate != null) QueryResult = DataCollection.Skip((int)Predicates.SkipPredicate).AsQueryable();

        if (Predicates.TakePredicate != null) QueryResult = DataCollection.Take((int)Predicates.TakePredicate).AsQueryable();

        if (Predicates.DistinctPredicate == true) QueryResult = DataCollection.Distinct().AsQueryable();

        if (Predicates.ReversePredicate == true) QueryResult = DataCollection.Reverse().AsQueryable();


        return QueryResult;
    }
};