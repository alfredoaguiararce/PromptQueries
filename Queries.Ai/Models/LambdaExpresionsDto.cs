using System.Linq.Expressions;

namespace Queries.Ai.Models;
public class LambdaExpresionsDto<T>
{
    public Expression<Func<T, bool>>? ExcecutableWhereLambda { get; set; }
    public Expression<Func<T, object>>? ExcecutableSelectLambda { get; set; }
    public Expression<Func<T, object>>? ExcecutableOrderByLambda { get; set; }
    public Expression<Func<T, object>>? ExcecutableOrderByDescendingLambda { get; set; }
    public Expression<Func<T, object>>? ExcecutableThenByLambda { get; set; }
    public Expression<Func<T, object>>? ExcecutableThenByDescendingLambda { get; set; }
    public Expression<Func<T, decimal>>? ExcecutableAverageLambda { get; set; }
    public Expression<Func<int>>? ExcecutableSkipLambda { get; set; }
    public Expression<Func<int>>? ExcecutableTakeLambda { get; set; }
    public bool? ExcecutableDistinctLambda { get; set; }
    public bool? ExcecutableReverseLambda { get; set; }
}
