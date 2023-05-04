using Microsoft.Extensions.DependencyInjection;

namespace Querya.Ai
{
    /* The class provides a method to authenticate an OpenAI API key in a C# application. */
    public static class QueriesOpenAi
    {
        /// <summary>
        /// This function adds a service to the IServiceCollection that provides authentication for an
        /// OpenAI API key.
        /// </summary>
        /// <param name="IServiceCollection">IServiceCollection is an interface provided by the
        /// Microsoft.Extensions.DependencyInjection namespace that defines a contract for adding and
        /// configuring services in an application's service container. It is used to register and
        /// configure dependencies that can be injected into classes and components throughout the
        /// application.</param>
        /// <param name="OPENAI_API_KEY">The OPENAI_API_KEY is a string parameter that represents the
        /// API key required to access the OpenAI API. This key is used to authenticate and authorize
        /// the user to access the OpenAI API services.</param>
        public static void Auth(this IServiceCollection services, string OPENAI_API_KEY)
        {
            services.AddScoped<IQueryPrompt, QueryPrompt>(x => new QueryPrompt(OPENAI_API_KEY));
        }
    }
}
