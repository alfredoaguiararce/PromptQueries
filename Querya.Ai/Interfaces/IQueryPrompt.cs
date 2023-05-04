public interface IQueryPrompt
{
    Task<IQueryable<T>> RunQuery<T>(IQueryable<T> Data, string Prompt);
}