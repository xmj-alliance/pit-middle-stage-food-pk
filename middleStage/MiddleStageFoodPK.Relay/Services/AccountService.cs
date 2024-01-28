using GraphQL;
using GraphQL.Client.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MiddleStageFoodPK.Model.Local;
using MiddleStageFoodPK.Model.Upstream;
using MiddleStageFoodPK.Relay.Context;
using System.Collections.Generic;
using System.Xml.Linq;

namespace MiddleStageFoodPK.Relay.Services;

public class AccountService(
    IConfiguration config,
    IGraphQLClientContext clientContext,
    ILogger<AccountService> logger
) : IAccountService
{
    private readonly IConfiguration config = config;
    private readonly IGraphQLClientContext clientContext = clientContext;
    private readonly ILogger<AccountService> logger = logger;
    private readonly GraphQLHttpClient client = clientContext.Client;

    public string GraphBasePath { get; init; } =
      Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "Graphs", "Upstream"));

    public async Task<List<Account>> GetAccountsByIDs(IList<string> ids)
    {
        string query = await File.ReadAllTextAsync(
            Path.Combine(GraphBasePath, "getAccountsByIDs.graphql")
        );

        var gqlRequest = new GraphQLRequest
        {
            Query = query,
            Variables = new
            {
                ids
            }
        };

        var graphQLResponse = await client.SendQueryAsync<SalesforceGQLRootQuery>(gqlRequest)
            ?? throw new Exception("Failed to get a GraphQL Response from the upstream.");

        if (graphQLResponse.Errors is { })
        {
            logger.LogError("[GQL]", graphQLResponse.Errors);
        }

        if (graphQLResponse.Data is null)
        {
            throw new Exception("Data of GraphQL response is null. Please check error logs.");
        }

        List<Account> transformedAccounts = (
            from account in graphQLResponse.Data.uiapi.query.Account.edges
            where account.node is { }
            select InboundAccountFromUpstream(account)

        ).ToList();

        return transformedAccounts;
    }

    public Account? InboundAccountFromUpstream(SalesforceGQLEdge<SalesforceGQLAccount> upstreamAccount)
    {
        SalesforceGQLAccount? node = upstreamAccount.node;
        if (node is null)
        {
            return null;
        }

        List<Contact>? contacts = null;
        var upstreamContacts = node.Contacts;

        if (upstreamContacts is { })
        {
            contacts = (
                from contact in upstreamContacts.edges
                where contact.node is { }
                select InboundContactFromUpstream(contact)
            ).ToList();
        }

        return new Account(
            Id: node.Id,
            Name: node.Name.value!,
            AverageStars: node.Average_Stars__c.value,
            Contacts: contacts
        );
    }

    public Contact? InboundContactFromUpstream(SalesforceGQLEdge<SalesforceGQLContact> upstreamContact)
    {
        SalesforceGQLContact? node = upstreamContact.node;
        if (node is null)
        {
            return null;
        }

        return new Contact(
            Id: node.Id,
            Name: node.Name.value!,
            Description: node.Description?.value
        );
    }

}
