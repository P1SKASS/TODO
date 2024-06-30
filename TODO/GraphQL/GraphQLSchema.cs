using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace TODO.GraphQL
{
    public class GraphQLSchema : Schema
    {
        public GraphQLSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<TodoQuery>();
            Mutation = provider.GetRequiredService<TodoMutation>();
        }
    }
}
