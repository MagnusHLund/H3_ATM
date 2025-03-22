using Hæveautomaten.Entities;
using Hæveautomaten.Interfaces.Repositories;

namespace HæveautomatenTests.Repositories
{
    public class InMemoryAccountRepository : IAccountRepository
    {
        private readonly List<AccountEntity> _accounts = new List<AccountEntity>();

        public bool CreateAccount(AccountEntity account)
        {
            _accounts.Add(account);
            return true;
        }

        public bool DeleteAccount(int accountId)
        {
            AccountEntity account = _accounts.FirstOrDefault(a => a.AccountId == accountId);
            if (account == null)
            {
                throw new KeyNotFoundException($"Account with ID {accountId} not found.");
            }

            _accounts.Remove(account);
            return true;
        }

        public List<AccountEntity> GetAccountsByPerson(int personId)
        {
            return _accounts.Where(a => a.AccountOwner.PersonId == personId).ToList();
        }

        public List<AccountEntity> GetAllAccounts()
        {
            return new List<AccountEntity>(_accounts);
        }

        public AccountEntity UpdateAccount(AccountEntity account)
        {
            AccountEntity existingAccount = _accounts.FirstOrDefault(a => a.AccountId == account.AccountId);
            if (existingAccount == null)
            {
                throw new KeyNotFoundException($"Account with ID {account.AccountId} not found.");
            }

            existingAccount.BalanceInMinorUnits = account.BalanceInMinorUnits;
            return existingAccount;
        }
    }
}