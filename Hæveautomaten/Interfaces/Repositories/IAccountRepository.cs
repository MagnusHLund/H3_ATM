using Hæveautomaten.Entities;

namespace Hæveautomaten.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        bool CreateAccount(AccountEntity account);
        bool DeleteAccount(uint accountId);
    }
}