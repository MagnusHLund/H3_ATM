using Hæveautomaten.Entities;

namespace Hæveautomaten.Interfaces
{
    public interface IAccountController
    {
        void CreateAccount();
        void DeleteAccount();
        List<AccountEntity> GetAllAccounts();
    }
}