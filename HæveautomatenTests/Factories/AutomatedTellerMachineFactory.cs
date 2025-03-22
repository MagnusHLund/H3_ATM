using Hæveautomaten.Entities;
using HæveautomatenTests.Utils;

namespace HæveautomatenTests.Factories
{
    internal static class AutomatedTellerMachineFactory
    {
        internal static AutomatedTellerMachineEntity CreateAutomatedTellerMachine()
        {
            return TestDataGeneratorUtils.AtmFaker.Generate();
        }

        internal static AutomatedTellerMachineEntity CreateAutomatedTellerMachine(
            BankEntity associatedBank = null,
            uint minimumExchangeAmount = 5
        )
        {
            associatedBank ??= BankFactory.CreateBank();

            AutomatedTellerMachineEntity atm = new AutomatedTellerMachineEntity(minimumExchangeAmount, associatedBank);

            return atm;
        }
    }
}