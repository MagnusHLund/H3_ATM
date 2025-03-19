using Hæveautomaten.Interfaces;
using Hæveautomaten.Entities;

namespace Hæveautomaten.Controllers
{
    public class AccountController : IAccountController
    {
        public void CreateAccount()
        {
            // Create a new account

            // First pick a bank
            // Then pick a person
            // Then create the account which connects the bank and the person.

            // Create bank and save it in the database
        }

        public void DeleteAccount()
        {
            // Get all accounts and store them in a variable
            // Select an account from the list and delete it
        }

        public List<AccountEntity> GetAllAccounts()
        {
            // Get all accounts and return them
            throw new NotImplementedException();
        }
    }
}