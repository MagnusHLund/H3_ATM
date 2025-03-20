using Hæveautomaten.Views;
using Hæveautomaten.Entities;
using Hæveautomaten.Interfaces.Controllers;
using Hæveautomaten.Interfaces.Repositories;

namespace Hæveautomaten.Controllers
{
    public class AccountController : IAccountController
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IBankController _bankController;
        private readonly IPersonController _personController;

        public AccountController(IAccountRepository accountRepository, IBankController bankController, IPersonController personController)
        {
            _accountRepository = accountRepository;
            _bankController = bankController;
            _personController = personController;
        }

        public bool CreateAccount()
        {
            long balance = long.Parse(CustomView.GetUserInputWithTitle("Enter balance: "));

            BankEntity bank = _bankController.SelectBank();
            PersonEntity person = _personController.SelectPerson();

            AccountEntity account = new AccountEntity(
                balanceInMinorUnits: balance,
                accountOwner: person,
                bank: bank
            );

            bool success = _accountRepository.CreateAccount(account);
            return success;
        }

        public List<AccountEntity> GetAccountsByPerson(PersonEntity person)
        {
            List<AccountEntity> accounts = _accountRepository.GetAccountsByPerson(person.PersonId);
            return accounts;
        }

        public AccountEntity SelectAccount(List<AccountEntity> accounts = null)
        {
            accounts ??= GetAllAccounts();

            string[] accountValues = accounts.Select(account => account.ToString()).ToArray();

            CustomView.CustomMenu(accountValues);

            string userInput = CustomView.GetUserInput();
            int accountIndex = int.Parse(userInput) - 1;

            return accounts[accountIndex];
        }

        public AccountEntity GetAccountByCard(CreditCardEntity creditCard)
        {
            List<AccountEntity> accounts = GetAllAccounts();
            AccountEntity account = accounts.Find(account => account.AccountId == creditCard.Account.AccountId) ?? new AccountEntity();

            return account;
        }

        public bool DeleteAccount()
        {
            AccountEntity account = SelectAccount();

            bool success = _accountRepository.DeleteAccount(account.AccountId);
            return success;
        }

        public List<AccountEntity> GetAllAccounts()
        {
            List<AccountEntity> accounts = _accountRepository.GetAllAccounts();
            return accounts;
        }
    }
}