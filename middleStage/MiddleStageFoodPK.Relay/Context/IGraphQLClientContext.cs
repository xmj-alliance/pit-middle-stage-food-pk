using GraphQL.Client.Http;

namespace MiddleStageFoodPK.Relay.Context;

public interface IGraphQLClientContext
{
    GraphQLHttpClient Client { get; }
}
