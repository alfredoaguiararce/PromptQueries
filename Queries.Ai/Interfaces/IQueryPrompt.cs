public interface IQueryPrompt
{
    Task<dynamic?> RunQuery<T>(IQueryable<T> Data, string Prompt);
}