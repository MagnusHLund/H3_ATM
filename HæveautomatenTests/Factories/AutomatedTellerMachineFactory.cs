using Hæveautomaten.Models;

namespace Hæveautomaten.Factories
{
    internal static class AutomatedTellerMachineFactory
    {
        internal static AutomatedTellerMachine CreateAutomatedTellerMachine(Bank associatedBank)
        {
            uint minimumExchangeAmount = 5;

            AutomatedTellerMachine atm = new AutomatedTellerMachine(minimumExchangeAmount);

            associatedBank.AddAutomatedTellerMachine(atm);

            return atm;
        }
    }
}