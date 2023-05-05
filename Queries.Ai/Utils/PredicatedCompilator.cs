using Queries.Ai.Models;

namespace Queries.Ai.Utils
{
    public class PredicatedCompilator<T>
    {
        public PredicatesDto<T> Compile<T>(LambdaExpresionsDto<T> LambdaExpresions)
        {
            if (LambdaExpresions == null) throw new ArgumentNullException("There's no lambdas to compile");
            PredicatesDto <T> Result = new PredicatesDto<T>();

            if (LambdaExpresions.ExcecutableWhereLambda != null) Result.WherePredicate = LambdaExpresions.ExcecutableWhereLambda.Compile();
            if (LambdaExpresions.ExcecutableSelectLambda != null) Result.SelectPredicate = LambdaExpresions.ExcecutableSelectLambda.Compile();
            if (LambdaExpresions.ExcecutableOrderByLambda != null) Result.OrderByPredicate = LambdaExpresions.ExcecutableOrderByLambda.Compile();
            if (LambdaExpresions.ExcecutableOrderByDescendingLambda != null) Result.OrderByDescendingPredicate = LambdaExpresions.ExcecutableOrderByDescendingLambda.Compile();
            if (LambdaExpresions.ExcecutableThenByLambda != null) Result.ThenByPredicate = LambdaExpresions.ExcecutableThenByLambda.Compile();
            if (LambdaExpresions.ExcecutableThenByDescendingLambda != null) Result.ThenByDescendingPredicate = LambdaExpresions.ExcecutableThenByDescendingLambda.Compile();
            if (LambdaExpresions.ExcecutableAverageLambda != null) Result.AveragePredicate = LambdaExpresions.ExcecutableAverageLambda.Compile();
            if (LambdaExpresions.ExcecutableSkipLambda != null) Result.SkipPredicate = LambdaExpresions.ExcecutableSkipLambda.Compile().Invoke();
            if (LambdaExpresions.ExcecutableTakeLambda != null) Result.TakePredicate = LambdaExpresions.ExcecutableTakeLambda.Compile().Invoke();
            if (LambdaExpresions.ExcecutableDistinctLambda == true) Result.DistinctPredicate = LambdaExpresions.ExcecutableDistinctLambda;
            if (LambdaExpresions.ExcecutableReverseLambda == true) Result.ReversePredicate = LambdaExpresions.ExcecutableReverseLambda;

            return Result;
        }
    }
}
