using Hæveautomaten.Entities;

namespace HæveautomatenTests.Factories
{
    internal static class AutomatedTellerMachineFactory
    {
        internal static AutomatedTellerMachineEntity CreateAutomatedTellerMachine(
            BankEntity associatedBank = null,
            uint minimumExchangeAmount = 5
        )
        {
            associatedBank ??= BankFactory.CreateBank();

            AutomatedTellerMachineEntity atm = new AutomatedTellerMachineEntity(minimumExchangeAmount);

            associatedBank.AutomatedTellerMachines.Add(atm);

            return atm;
        }
    }
}