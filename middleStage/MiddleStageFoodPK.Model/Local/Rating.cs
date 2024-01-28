namespace MiddleStageFoodPK.Model.Local;

public record Rating(
    string Id,
    string Name,
    Account Account,
    Contact Contact,
    string? Title,
    string? Description,
    double? Stars
);