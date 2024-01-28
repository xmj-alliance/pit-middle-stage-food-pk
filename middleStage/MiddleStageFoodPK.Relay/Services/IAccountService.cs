using MiddleStageFoodPK.Model.Local;

namespace MiddleStageFoodPK.Relay.Services;

public interface IAccountService
{
    Task<List<Account>> GetAccountsByIDs(IList<string> ids);
}