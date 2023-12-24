#pragma warning disable IDE1006 // Naming Styles

using System.Text.Json;

namespace MiddleStageFoodPK.Model.Upstream;

public record SalesforceGQLRootQuery(
    SalesforceGQLUiApi uiapi
);

public record SalesforceGQLUiApi(
    JsonElement aggregate,
    JsonElement[] objectInfos,
    SalesforceGQLRecordQuery query,
    JsonElement relatedListByName
);

public record SalesforceGQLRecordQuery(
    SalesforceGQLConnection<SalesforceGQLAccount> Account,
    SalesforceGQLConnection<SalesforceGQLContact> Contact,
    SalesforceGQLConnection<SalesforceGQLRating__c> Rating__c
);

public record SalesforceGQLAccount(
    string Id,
    SalesforceGQLValue<string> Name,
    SalesforceGQLValue<double> Average_Stars__c,
    SalesforceGQLConnection<SalesforceGQLContact> Contacts
);

public record SalesforceGQLContact(
    string Id,
    SalesforceGQLValue<string> Name
);

public record SalesforceGQLRating__c(
    string Id,
    SalesforceGQLValue<string> Name
);

#region abstractions
public record SalesforceGQLConnection<T>(
    SalesforceGQLEdge<T>?[] edges,
    JsonElement pageInfo,
    int totalCount
);

public record SalesforceGQLEdge<T>(
    string cursor,
    T? node
);

public record SalesforceGQLValue<T>(
    string displayValue,
    string label,
    T value
);

#endregion abstractions


#pragma warning restore IDE1006 // Naming Styles
