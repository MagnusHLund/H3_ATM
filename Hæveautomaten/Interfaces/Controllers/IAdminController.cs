namespace Hæveautomaten.Interfaces.Controllers
{
    public interface IAdminController
    {
        void HandleAdminMenuDisplay();
        void HandleAdminMenu(string input);
        void CreateAccount();
        void DeleteAccount();
        void CreateAutomatedTellerMachine();
        void DeleteAutomatedTellerMachine();
        void CreateBank();
        void DeleteBank();
        void CreateCreditCard();
        void DeleteCreditCard();
        void CreatePerson();
        void DeletePerson();
    }
}