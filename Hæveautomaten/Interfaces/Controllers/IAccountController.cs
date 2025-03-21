using Hæveautomaten.Entities;

namespace Hæveautomaten.Interfaces.Controllers
{
    public interface IAccountController
    {
        bool CreateAccount();
        bool DeleteAccount();
        List<AccountEntity> GetAccountsByPerson(PersonEntity person);
        AccountEntity SelectAccount(List<AccountEntity> accounts = null);
        AccountEntity GetAccountByCard(CreditCardEntity creditCard);
        List<AccountEntity> GetAllAccounts();
        AccountEntity UpdateAccount(AccountEntity account);
    }
}