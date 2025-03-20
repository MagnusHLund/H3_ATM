using Hæveautomaten.Entities;

namespace Hæveautomaten.Interfaces.Controllers
{
    public interface IAutomatedTellerMachineController
    {
        void HandleAutomatedTellerMachineMenu();
        bool CreateAutomatedTellerMachine();
        bool DeleteAutomatedTellerMachine();
        void UseAutomatedTellerMachine(AutomatedTellerMachineEntity atm);
        AutomatedTellerMachineEntity SwitchAutomatedTellerMachine(List<AutomatedTellerMachineEntity> atms);
        List<AutomatedTellerMachineEntity> GetAllAutomatedTellerMachines();
        AutomatedTellerMachineEntity SelectAutomatedTellerMachine();
        void DepositMoney(AutomatedTellerMachineEntity atm, CreditCardEntity creditCard);
        void WithdrawMoney(AutomatedTellerMachineEntity atm, CreditCardEntity creditCard);
    }
}