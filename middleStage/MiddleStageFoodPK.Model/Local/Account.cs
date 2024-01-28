namespace MiddleStageFoodPK.Model.Local;

public record Account(
    string Id,
    string Name,
    double? AverageStars,
    IList<Contact>? Contacts
);
