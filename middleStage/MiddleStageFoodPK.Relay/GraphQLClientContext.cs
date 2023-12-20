using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace MiddleStageFoodPK.Relay;

public class GraphQLClientContext : IGraphQLClientContext
{
    private readonly HttpClient httpClient;
    private readonly Uri endPoint;

    public GraphQLHttpClient Client { get; init; }

    public GraphQLClientContext(
        IHttpClientFactory factory
    )
    {
        httpClient = factory.CreateClient("client");
        endPoint = new Uri($"{httpClient.BaseAddress}/graphql");

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
