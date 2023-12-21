using MiddleStageFoodPK.BlazorApp.Client.Pages;
using MiddleStageFoodPK.BlazorApp.Components;
using MiddleStageFoodPK.Relay.Context;
using MiddleStageFoodPK.Relay.Services;

var builder = WebApplication.CreateBuilder(args);

string upstreamBaseURL = builder.Configuration.GetValue<string>("upstreamBaseURL")
                            ?? throw new ApplicationException("upstreamBaseURL cannot be null");
string upstreamAuthEndpoint = builder.Configuration.GetValue<string>("upstreamAuthEndpoint")
                            ?? throw new ApplicationException("upstreamAuthEndpoint cannot be null");
string upstreamAuthClientID = builder.Configuration.GetValue<string>("upstreamAuthClientID")
                            ?? throw new ApplicationException("upstreamAuthClientID cannot be null");
string upstreamAuthClientSecret = builder.Configuration.GetValue<string>("upstreamAuthClientSecret")
                            ?? throw new ApplicationException("upstreamAuthClientSecret cannot be null");
string upstreamAuthClientScope = builder.Configuration.GetValue<string>("upstreamAuthClientScope")
                            ?? throw new ApplicationException("upstreamAuthClientScope cannot be null");

builder.Services.AddClientCredentialsTokenManagement()
    .AddClient("salesforce.client", client =>
    {
        client.TokenEndpoint = upstreamAuthEndpoint;
        client.ClientId = upstreamAuthClientID;
        client.ClientSecret = upstreamAuthClientSecret;
        client.Scope = upstreamAuthClientScope;
    });

// Add identity management
builder.Services.AddClientCredentialsHttpClient("salesforce", "salesforce.client", client =>
{
    client.BaseAddress = new Uri(upstreamBaseURL);
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddDistributedMemoryCache();

// Configuring services
builder.Services.AddSingleton<IDataAccessService, DataAccessService>();
builder.Services.AddSingleton<IGraphQLClientContext, GraphQLClientContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);

app.Run();
