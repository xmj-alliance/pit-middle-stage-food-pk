#pragma warning disable IDE1006 // Naming Styles

using System.Text.Json;

namespace MiddleStageFoodPK.Model.Upstream;

#region gqlModels

public record SalesforceGQLAccount(
    string Id,
    SalesforceGQLValue<string> Name,
    SalesforceGQLValue<double?> Average_Stars__c,
    SalesforceGQLConnection<SalesforceGQLContact> Contacts
);

public record SalesforceGQLContact(
    string Id,
    SalesforceGQLValue<string> Name,
    SalesforceGQLValue<string> Description
);

public record SalesforceGQLRating__c(
    string Id,
    SalesforceGQLValue<string> Name,
    SalesforceGQLValue<string> Account__c,
    SalesforceGQLValue<string> Contact__c,
    SalesforceGQLValue<string> Title__c,
    SalesforceGQLValue<string> Description__c,
    SalesforceGQLValue<double> Stars__c
);

#endregion gqlModels

#region queries

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

#endregion queries

#region mutations

public record SalesforceGQLRootMutation(
    SalesforceGQLUiApiMutations uiapi
);

public record SalesforceGQLUiApiMutations(
    SalesforceGQLCreatePayload<SalesforceGQLAccount> AccountCreate,
    SalesforceGQLCreatePayload<SalesforceGQLContact> ContactCreate,
    SalesforceGQLCreatePayload<SalesforceGQLRating__c> Rating__cCreate
);

#endregion mutations

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
    T? value
);

public record SalesforceGQLCreatePayload<T>(
    T Record
);

public record SalesforceGQLDeletePayload<T>(
    string Id
);

public record SalesforceGQLUpdatePayload<T>(
    bool success
);

#endregion abstractions


#pragma warning restore IDE1006 // Naming Styles
