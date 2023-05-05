
namespace Queries.Ai.Models;
public class PredicatesDto<T>
{
    public Func<T, bool>? WherePredicate { get; set; }
    public Func<T, object>? SelectPredicate { get; set; }
    public Func<T, object>? OrderByPredicate { get; set; }
    public Func<T, object>? OrderByDescendingPredicate { get; set; }
    public Func<T, object>? ThenByPredicate { get; set; }
    public Func<T, object>? ThenByDescendingPredicate { get; set; }
    public Func<T, decimal>? AveragePredicate { get; set; }
    public int? SkipPredicate { get; set; }
    public int? TakePredicate { get; set; }
    public bool? DistinctPredicate { get; set; }
    public bool? ReversePredicate { get; set; }
}