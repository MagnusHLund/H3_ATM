using Hæveautomaten.Entities;

namespace Hæveautomaten.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        bool CreateAccount(AccountEntity account);
        bool DeleteAccount(int accountId);
        List<AccountEntity> GetAccountsByPerson(int personId);
        List<AccountEntity> GetAllAccounts();
        AccountEntity UpdateAccount(AccountEntity account);
    }
}