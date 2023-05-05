using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using OpenAI.API;
using OpenAI.API.Completions;
using OpenAI.API.Models;
using Queries.Ai.Models;

namespace Queries.Ai.Utils;

public class QueryPrompt: IQueryPrompt
{

    private readonly string API_KEY;

    public QueryPrompt(string OPEN_API_KEY)
    {
        this.API_KEY = OPEN_API_KEY;
    }


    public async Task<IQueryable<T>> RunQuery<T>(IQueryable<T> DataCollection, string Prompt)
    {
        
        /* `OpenAIAPI openai = new OpenAIAPI(API_KEY);` is creating a new instance of the `OpenAIAPI`
        class and assigning it to the variable `openai`. The `OpenAIAPI` class is used to interact
        with the OpenAI API, and the `API_KEY` parameter is used to authenticate and authorize
        access to the API. */
        OpenAIAPI openai = new OpenAIAPI(new APIAuthentication(API_KEY));

        Type[] Types = GetTypes(DataCollection);
        /* This code is using the OpenAI API to generate a completion for a given prompt. The `openai`
        object is an instance of the `OpenAIAPI` class, which is used to interact with the OpenAI
        API. The `Completions` property of the `openai` object is used to create a new completion
        request using the `CreateCompletionAsync` method. */
        string prompt = $"Generate LINQ query as a lambda expression in c# over IQueryable<{DataCollection.GetType()}> {GetCollectionText(DataCollection)} parameter for {Prompt}. Return only the lambda expression over the DataCollection parameter.";

        var response = await openai.Completions.CreateCompletionAsync(
            new CompletionRequest(
                prompt: prompt,
                model: Model.DavinciText,
                max_tokens: 60,
                temperature: 0.1,
                numOutputs: 1
                //stopSequences: new string[] { "\n" }
                )
            );
        string LinqQuery = response.Completions[0].Text.Trim();

        LinqQueryResultDTO Dto = new LinqQueryParser().ParseLinqQuery( LinqQuery );

        LambdaExpresionsDto<T> lambdaExpresionsDto = new LambdaParser<T>().GetExecutableExpresions<T>(Dto);

        PredicatesDto<T> CompilatePredicates = new PredicatedCompilator<T>().Compile<T>(lambdaExpresionsDto);

        IQueryable<T> QueryResult = new QueryExecutor<T>().RunQueries(DataCollection, CompilatePredicates);

        return QueryResult;
    }

    private string GetCollectionText<T>(IQueryable<T> collection)
    {
        PropertyInfo[] properties = typeof(T).GetProperties();
        string datalist = "";
        foreach (T item in collection)
        {
            string itemData = "";
            foreach (PropertyInfo property in properties)
            {
                itemData += $"{property.Name}: {property.GetValue(item)}, ";
            }
            itemData = itemData.TrimEnd(',', ' ');
            datalist += $"{itemData}\n";
        }
        return datalist;
    }

    private Type[] GetTypes<T>(IQueryable<T> collection)
    {
        PropertyInfo[] properties = typeof(T).GetProperties();
        Type[] types = new Type[properties.Length];
        for (int i = 0; i < properties.Length; i++)
        {
            types[i] = properties[i].PropertyType.GetTypeInfo();
        }
        return types;
    }
}
