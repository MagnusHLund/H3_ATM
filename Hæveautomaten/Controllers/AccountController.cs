using Hæveautomaten.Entities;
using Hæveautomaten.Interfaces.Controllers;

namespace Hæveautomaten.Controllers
{
    public class AccountController : IAccountController
    {
        public bool CreateAccount(AccountEntity account)
        {
            // Create account and save it in the database

            throw new NotImplementedException();
        }

        public bool DeleteAccount(AccountEntity account)
        {
            // Delete the account from the database

            throw new NotImplementedException();
        }

        public List<AccountEntity> GetAllAccounts()
        {
            // Get all accounts and return them
            throw new NotImplementedException();
        }
    }
}