using GraphQL.Client.Http;

namespace MiddleStageFoodPK.Relay;

public interface IGraphQLClientContext
{
    GraphQLHttpClient Client { get; }
}
