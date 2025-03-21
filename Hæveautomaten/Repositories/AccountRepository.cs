using Hæveautomaten.Data;
using Hæveautomaten.Entities;
using Microsoft.EntityFrameworkCore;
using Hæveautomaten.Interfaces.Repositories;

namespace Hæveautomaten.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly HæveautomatenDbContext _context;

        public AccountRepository(HæveautomatenDbContext context)
        {
            _context = context;
        }

        public bool CreateAccount(AccountEntity account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteAccount(int accountId)
        {
            AccountEntity? account = _context.Accounts.Find(accountId);

            if (account == null)
            {
                throw new KeyNotFoundException($"Account with ID {accountId} not found.");
            }

            _context.Accounts.Remove(account);
            _context.SaveChanges();
            return true;
        }

        public List<AccountEntity> GetAccountsByPerson(int personId)
        {
            List<AccountEntity> accounts = _context.Accounts
                .Include(a => a.Bank)
                .Include(a => a.CreditCards)
                .Where(a => a.AccountOwner.PersonId == personId)
                .ToList();

            return accounts;
        }

        public List<AccountEntity> GetAllAccounts()
        {
            List<AccountEntity> accounts = _context.Accounts
                .Include(a => a.Bank)
                .Include(a => a.CreditCards)
                .Include(a => a.AccountOwner)
                .ToList();

            return accounts;
        }

        public AccountEntity UpdateAccount(AccountEntity account)
        {
            AccountEntity updatedAccount = _context.Accounts.Update(account).Entity;
            _context.SaveChanges();
            return updatedAccount;
        }
    }
}