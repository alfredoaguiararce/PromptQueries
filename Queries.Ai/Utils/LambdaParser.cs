using Queries.Ai.Models;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Queries.Ai.Utils;
public class LambdaParser<T>
{
    public LambdaParser() { }

    public LambdaExpresionsDto<T> GetExecutableExpresions<T>(LinqQueryResultDTO dto)
    {
        if (dto == null) throw new ArgumentNullException("There's no lambdas to convert");

        LambdaExpresionsDto<T> Result = new LambdaExpresionsDto<T>();

        if (dto.WhereLambda != null) Result.ExcecutableWhereLambda = GetExecutableExpression<T, bool>(dto.WhereLambda);
        if (dto.SelectLambda != null) 
        {
            try
            {
                Result.ExcecutableSelectLambda = GetExecutableExpression<T, object>(dto.SelectLambda);
            }catch (Exception ex)
            {
                //GetExecutableExpresions<T, T>(dto.SelectLambda);
            }
            

        }
        if (dto.OrderByLambda != null) Result.ExcecutableOrderByLambda = GetExecutableExpression<T, object>(dto.OrderByLambda);
        if (dto.OrderByDescendingLambda != null) Result.ExcecutableOrderByDescendingLambda = GetExecutableExpression<T, object>(dto.OrderByDescendingLambda);
        if (dto.ThenByLambda != null) Result.ExcecutableThenByLambda = GetExecutableExpression<T, object>(dto.ThenByLambda);
        if (dto.ThenByDescendingLambda != null) Result.ExcecutableThenByDescendingLambda = GetExecutableExpression<T, object>(dto.ThenByDescendingLambda);
        if (dto.SkipLambda != null) Result.ExcecutableSkipLambda = GetExecutableInt<T>(dto.SkipLambda);
        if (dto.TakeLambda != null) Result.ExcecutableTakeLambda = GetExecutableInt<T>(dto.TakeLambda);
        if (dto.AverageLambda != null) Result.ExcecutableAverageLambda = GetExecutableExpression<T, decimal>(dto.AverageLambda);
        if (dto.DistinctLambda != null) Result.ExcecutableDistinctLambda = true;
        if (dto.ReverseLambda != null) Result.ExcecutableReverseLambda = true;

        return Result;
    }

    private Expression<Func<T, T2>>? GetExecutableExpression<T, T2>(string Expression)
    {
        Expression<Func<T, T2>>? LambdaExpr = DynamicExpressionParser.ParseLambda<T, T2>(new ParsingConfig(), true, Expression);
        return LambdaExpr;
    }

    private Expression<Func<int>>? GetExecutableInt<T>(string Expression)
    {
        Expression<Func<int>>? Result = DynamicExpressionParser.ParseLambda<int>(new ParsingConfig(), true, expression: Expression);
        return Result;
    }

}
