using Hæveautomaten.Entities;

namespace Hæveautomaten.Interfaces.Controllers
{
    public interface IAutomatedTellerMachineController
    {
        void HandleAutomatedTellerMachineMenu();
        bool CreateAutomatedTellerMachine(AutomatedTellerMachineEntity atm);
        bool DeleteAutomatedTellerMachine(AutomatedTellerMachineEntity atm);
        void UseAutomatedTellerMachine(AutomatedTellerMachineEntity atm);
        AutomatedTellerMachineEntity SwitchAutomatedTellerMachine(List<AutomatedTellerMachineEntity> atms);
        List<AutomatedTellerMachineEntity> GetAllAutomatedTellerMachines();
        void DepositMoney(AutomatedTellerMachineEntity atm, CreditCardEntity creditCard);
        void WithdrawMoney(AutomatedTellerMachineEntity atm, CreditCardEntity creditCard);
    }
}