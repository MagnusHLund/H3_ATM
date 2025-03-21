using Hæveautomaten.Views;
using Hæveautomaten.Entities;
using Hæveautomaten.Interfaces.Views;
using Hæveautomaten.Interfaces.Controllers;
using Hæveautomaten.Interfaces.Repositories;

namespace Hæveautomaten.Controllers
{
    public class AccountController : IAccountController
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPersonController _personController;
        private readonly IBankController _bankController;
        private readonly IBaseView _baseView;

        public AccountController(
            IAccountRepository accountRepository,
            IBankController bankController,
            IPersonController personController,
            IBaseView baseView
        )
        {
            _accountRepository = accountRepository;
            _bankController = bankController;
            _personController = personController;
            _baseView = baseView;
        }

        public bool CreateAccount()
        {
            long balance = long.Parse(_baseView.GetUserInputWithTitle("Enter balance:"));

            BaseView.DisplayHeader("Select bank:");
            BankEntity bank = _bankController.SelectBank();

            BaseView.DisplayHeader("Select person:");
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

            _baseView.CustomMenu(accountValues);

            string userInput = _baseView.GetUserInput();
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

        public AccountEntity UpdateAccount(AccountEntity account)
        {
            AccountEntity updatedAccount = _accountRepository.UpdateAccount(account);
            return updatedAccount;
        }
    }
}