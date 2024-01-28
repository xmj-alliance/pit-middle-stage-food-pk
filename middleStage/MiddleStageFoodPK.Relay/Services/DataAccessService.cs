using GraphQL;
using GraphQL.Client.Http;
using Microsoft.Extensions.Logging;
using MiddleStageFoodPK.Model.Upstream;
using MiddleStageFoodPK.Relay.Context;

namespace MiddleStageFoodPK.Relay.Services;

public class DataAccessService : IDataAccessService
{
    private readonly GraphQLHttpClient client;
    private readonly ILogger<DataAccessService> logger;

    public string GraphBasePath { get; init; } =
      Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "Graphs", "Upstream"));

    public DataAccessService(
        ILogger<DataAccessService> logger,
        IGraphQLClientContext clientContext
    )
    {
        this.logger = logger;
        client = clientContext.Client;

    }

    public async Task TestGetAccounts()
    {
        string query = await File.ReadAllTextAsync(
            Path.Combine(GraphBasePath, "getAccountsByIDs.graphql")
        );

        var gqlRequest = new GraphQLRequest
        {
            Query = query,
            Variables = new {
                ids = new string[]
                {
                    "0018b00002bOgwYAAS",
                    "0018b00002bOgwZAAS"
                }
            }
        };

        var graphQLResponse = await client.SendQueryAsync<SalesforceGQLRootQuery>(gqlRequest);

        await Console.Out.WriteLineAsync("yes");

    }

    public async Task TestAddAccount()
    {
        string mutation = await File.ReadAllTextAsync(
            Path.Combine(GraphBasePath, "addAccount.graphql")
        );

        var gqlRequest = new GraphQLRequest
        {
            Query = mutation,
            Variables = new
            {
                newItem = new
                {
                    Account = new
                    {
                        Name = "Meow Meow Pizza",
                        Description = "Freshly baked meow-meow styled Pizza"
                    }
                },
                option = new
                {
                    allOrNone = true
                }
            }
        };

        var graphQLResponse = await client.SendMutationAsync<SalesforceGQLRootMutation>(gqlRequest);

        await Console.Out.WriteLineAsync("yes");

    }
}
