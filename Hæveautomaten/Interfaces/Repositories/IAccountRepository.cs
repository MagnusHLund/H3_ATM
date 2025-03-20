using Hæveautomaten.Entities;

namespace Hæveautomaten.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        bool CreateAccount(AccountEntity account);
        bool DeleteAccount(uint accountId);
        List<AccountEntity> GetAccountsByPerson(uint personId);
        List<AccountEntity> GetAllAccounts();
    }
}