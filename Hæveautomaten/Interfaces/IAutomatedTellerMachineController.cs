using Hæveautomaten.Entities;

namespace Hæveautomaten.Interfaces
{
    public interface IAutomatedTellerMachineController
    {
        void HandleAutomatedTellerMachineMenu();
        void CreateAutomatedTellerMachine();
        void DeleteAutomatedTellerMachine();
        void UseAutomatedTellerMachine(AutomatedTellerMachineEntity atm);
        AutomatedTellerMachineEntity SwitchAutomatedTellerMachine(List<AutomatedTellerMachineEntity> atms);
        List<AutomatedTellerMachineEntity> GetAllAutomatedTellerMachines();
        void DepositMoney(AutomatedTellerMachineEntity atm, CreditCardEntity creditCard);
        void WithdrawMoney(AutomatedTellerMachineEntity atm, CreditCardEntity creditCard);
    }
}