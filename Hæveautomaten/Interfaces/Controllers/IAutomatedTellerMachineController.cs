using Hæveautomaten.Entities;

namespace Hæveautomaten.Interfaces.Controllers
{
    public interface IAutomatedTellerMachineController
    {
        void HandleAutomatedTellerMachineMenu();
        bool CreateAutomatedTellerMachine();
        bool DeleteAutomatedTellerMachine();
        void UseAutomatedTellerMachine(AutomatedTellerMachineEntity atm, CreditCardEntity creditCard, AccountEntity account);
        List<AutomatedTellerMachineEntity> GetAllAutomatedTellerMachines();
        AutomatedTellerMachineEntity SelectAutomatedTellerMachine();
        long DepositMoney(AutomatedTellerMachineEntity atm, AccountEntity accountEntity);
        long WithdrawMoney(AutomatedTellerMachineEntity atm, AccountEntity accountEntity);
    }
}