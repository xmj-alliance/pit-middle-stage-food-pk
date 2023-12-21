using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace MiddleStageFoodPK.Relay.Context;

public class GraphQLClientContext : IGraphQLClientContext
{
    private readonly HttpClient httpClient;
    private readonly Uri endPoint;

    public GraphQLHttpClient Client { get; init; }

    public GraphQLClientContext(
        IHttpClientFactory factory
    )
    {
        httpClient = factory.CreateClient("salesforce");
        endPoint = new Uri($"{httpClient.BaseAddress}/services/data/v59.0/graphql");

        Client = new GraphQLHttpClient(
            new GraphQLHttpClientOptions
            {
                EndPoint = endPoint,
                UseWebSocketForQueriesAndMutations = false
            },
            new SystemTextJsonSerializer(
                new JsonSerializerOptions()
                {
                    Converters = {
                        new JsonStringEnumConverter(new ConstantCaseJsonNamingPolicy(), false)
                    }
                }.SetupImmutableConverter()
            ),
            httpClient
        );
    }

}
