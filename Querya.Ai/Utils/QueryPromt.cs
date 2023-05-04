﻿using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using OpenAI.API;
using OpenAI.API.Completions;
using OpenAI.API.Models;

public class QueryPrompt: IQueryPrompt
{
    /* `private readonly string API_KEY;` is declaring a private instance variable `API_KEY` of type
    `string` with the `readonly` modifier. This means that the value of `API_KEY` can only be set
    once, either in the constructor or at the point of declaration, and cannot be changed
    afterwards. The `API_KEY` is used to authenticate and authorize access to the OpenAI API. */
    private readonly string API_KEY;

    public QueryPrompt(string OPEN_API_KEY)
    {
        this.API_KEY = OPEN_API_KEY;
    }

    /// <summary>
    /// This function generates a LINQ query based on a prompt using OpenAI's API and returns the result
    /// of executing the query on a given data collection.
    /// </summary>
    /// <param name="DataCollection">An IQueryable collection of data that you want to query using
    /// LINQ.</param>
    /// <param name="Prompt">The prompt is a string that describes the query you want to generate using
    /// OpenAI's GPT-3 language model. It should be a clear and concise description of the data you want
    /// to filter or manipulate using LINQ.</param>
    /// <returns>
    /// The method `RunQuery` returns an `IQueryable<T>` object, which represents a queryable collection
    /// of elements of type `T`. The queryable collection is the result of applying a LINQ query
    /// generated from a prompt string to an input `IQueryable<T>` collection.
    /// </returns>
    public async Task<IQueryable<T>> RunQuery<T>(IQueryable<T> DataCollection, string Prompt)
    {
        
        /* `OpenAIAPI openai = new OpenAIAPI(API_KEY);` is creating a new instance of the `OpenAIAPI`
        class and assigning it to the variable `openai`. The `OpenAIAPI` class is used to interact
        with the OpenAI API, and the `API_KEY` parameter is used to authenticate and authorize
        access to the API. */
        OpenAIAPI openai = new OpenAIAPI(API_KEY);

        /* This code is using the OpenAI API to generate a completion for a given prompt. The `openai`
        object is an instance of the `OpenAIAPI` class, which is used to interact with the OpenAI
        API. The `Completions` property of the `openai` object is used to create a new completion
        request using the `CreateCompletionAsync` method. */
        var response = await openai.Completions.CreateCompletionAsync(
            new CompletionRequest(
                prompt: $"Generate LINQ query for: {Prompt}.",
                model: Model.DavinciText,
                max_tokens: 60,
                temperature: 0.1,
                numOutputs: 1,
                stopSequences: new string[] { "\n" }
                )
            );

        
        /* `string LinqQuery = response.Completions[0].Text.Trim();` is assigning the generated LINQ
        query string to the variable `LinqQuery`. The `response.Completions[0].Text` property
        contains the generated text from the OpenAI API, which is the LINQ query in this case. The
        `Trim()` method is used to remove any leading or trailing white space from the generated
        text before assigning it to the `LinqQuery` variable. */
        string LinqQuery = response.Completions[0].Text.Trim();

        /* `LambdaExpression LambdaExpr = DynamicExpressionParser.ParseLambda(typeof(T), typeof(bool),
        LinqQuery);` is using the `DynamicExpressionParser` class from the
        `System.Linq.Dynamic.Core` namespace to parse the LINQ query string generated by the OpenAI
        API into a `LambdaExpression` object. The `LambdaExpression` object represents a lambda
        expression that can be compiled and executed to filter or manipulate data in a LINQ query.
        The `typeof(T)` parameter specifies the type of the input data collection, and the
        `typeof(bool)` parameter specifies the return type of the lambda expression. The `LinqQuery`
        parameter is the LINQ query string generated by the OpenAI API. */
        LambdaExpression LambdaExpr = DynamicExpressionParser.ParseLambda(typeof(T), typeof(bool), LinqQuery);

        /* `Expression<Func<T, bool>>? ExecutableExpr = LambdaExpr as Expression<Func<T, bool>>;` is
        attempting to cast the `LambdaExpr` object, which represents a lambda expression generated
        from the OpenAI API, to an `Expression<Func<T, bool>>` object. The `Expression<Func<T,
        bool>>` object is a strongly-typed lambda expression that can be compiled and executed to
        filter or manipulate data in a LINQ query. The `?` after the type indicates that the
        variable `ExecutableExpr` is nullable, meaning it can be assigned a null value. If the cast
        is successful, the lambda expression is assigned to the `ExecutableExpr` variable, which can
        then be compiled and executed to filter or manipulate data in a LINQ query. If the cast is
        not successful, an `InvalidOperationException` is thrown, indicating that the generated LINQ
        query is not valid. */
        Expression<Func<T, bool>>? ExecutableExpr = LambdaExpr as Expression<Func<T, bool>>;

        /* The `if(ExecutableExpr == null ) throw new InvalidOperationException("The generated LINQ
        query is not valid.");` statement is checking if the `LambdaExpr` object, which represents a
        lambda expression generated from the OpenAI API and parsed into a `LambdaExpression` object
        using the `DynamicExpressionParser`, can be cast to an `Expression<Func<T, bool>>` object.
        If the cast is not successful, meaning the generated LINQ query is not valid, an
        `InvalidOperationException` is thrown with the message "The generated LINQ query is not
        valid." This ensures that only valid LINQ queries are executed on the input data collection. */
        if(ExecutableExpr == null ) throw new InvalidOperationException("The generated LINQ query is not valid.");

        /* `Func<T, bool> Predicate = ExecutableExpr.Compile();` is compiling the `ExecutableExpr`
        lambda expression generated from the OpenAI API into a `Func<T, bool>` delegate. The
        `Func<T, bool>` delegate represents a method that takes an input of type `T` and returns a
        `bool` value. In this case, the `Func<T, bool>` delegate represents a predicate function
        that can be used to filter data in a LINQ query. The `Compile()` method of the
        `ExecutableExpr` lambda expression compiles the lambda expression into a delegate that can
        be executed at runtime. The resulting delegate is assigned to the `Predicate` variable,
        which can then be used to filter data in a LINQ query. */
        Func<T, bool> Predicate = ExecutableExpr.Compile();

        /* `IQueryable<T> QueryResult = DataCollection.Where(Predicate).AsQueryable<T>();` is applying
        the generated LINQ query to the input `IQueryable<T>` collection `DataCollection` by
        filtering the collection using the `Predicate` function generated from the OpenAI API. The
        `Where` method of the `DataCollection` object is used to filter the collection using the
        `Predicate` function, which takes an input of type `T` and returns a `bool` value. The
        resulting filtered collection is then converted to an `IQueryable<T>` object using the
        `AsQueryable<T>` method and assigned to the `QueryResult` variable. The `QueryResult`
        variable represents the result of applying the generated LINQ query to the input data
        collection. */
        IQueryable<T> QueryResult = DataCollection.Where(Predicate).AsQueryable<T>();

        return QueryResult;
    }
}
