using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Types.Relay.DataObjects;
using Microsoft.Extensions.Logging;
using MiddleStageFoodPK.Model.Upstream;
using MiddleStageFoodPK.Relay.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MiddleStageFoodPK.Relay.Services;

public class DataAccessService : IDataAccessService
{
    private readonly GraphQLHttpClient client;
    private readonly ILogger<DataAccessService> logger;

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
        string query = """
        {
          uiapi {
            query {
              Account {
                edges {
                  node {
                    Id
                    Name {
                      value
                    }
                    Contacts {
                      edges {
                        node {
                          Id
                          Name {
                            value
                          }
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
        """;

        var gqlRequest = new GraphQLRequest
        {
            Query = query,
            Variables = { }
        };

        var graphQLResponse = await client.SendQueryAsync<SalesforceGQLRootQuery>(gqlRequest);

        await Console.Out.WriteLineAsync("yes");

    }
}
