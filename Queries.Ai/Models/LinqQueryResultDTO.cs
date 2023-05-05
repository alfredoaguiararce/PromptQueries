namespace Queries.Ai.Models;

public class LinqQueryResultDTO
{
    public string? WhereLambda { get; set; }
    public string? SelectLambda { get; set; }
    public string? OrderByLambda { get; set; }
    public string? OrderByDescendingLambda { get; set; }
    public string? ThenByLambda { get; set; }
    public string? ThenByDescendingLambda { get; set; }
    public string? SkipLambda { get; set; }
    public string? TakeLambda { get; set; }
    public string? DistinctLambda { get; set; }
    public string? ReverseLambda { get; set; }
    public string? AverageLambda { get; set; }
}