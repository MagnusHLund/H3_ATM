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
            try
            {
                _context.Accounts.Add(account);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating account: {ex.Message}");
                return false;
            }
        }

        public bool DeleteAccount(uint accountId)
        {
            try
            {
                AccountEntity account = _context.Accounts.Find(accountId);
                if (account == null)
                {
                    throw new KeyNotFoundException($"Account with ID {accountId} not found.");
                }

                _context.Accounts.Remove(account);
                _context.SaveChanges();
                return true;
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Error deleting account: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting account: {ex.Message}");
                return false;
            }
        }

        public List<AccountEntity> GetAccountsByPerson(uint personId)
        {
            try
            {
                return _context.Accounts
                    .Include(a => a.Bank)
                    .Include(a => a.CreditCards)
                    .Where(a => a.AccountOwner.PersonId == personId)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving accounts for person ID {personId}: {ex.Message}");
                return new List<AccountEntity>();
            }
        }

        public List<AccountEntity> GetAllAccounts()
        {
            try
            {
                return _context.Accounts
                    .Include(a => a.Bank)
                    .Include(a => a.CreditCards)
                    .Include(a => a.AccountOwner)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving all accounts: {ex.Message}");
                return new List<AccountEntity>();
            }
        }
    }
}