using Hæveautomaten.Entities;

namespace Hæveautomaten.Interfaces.Controllers
{
    public interface IAccountController
    {
        bool CreateAccount(AccountEntity account);
        bool DeleteAccount(AccountEntity account);
        List<AccountEntity> GetAllAccounts();
    }
}