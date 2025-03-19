using Hæveautomaten.Entities;
using Hæveautomaten.Interfaces;

namespace Hæveautomaten.Controllers
{
    public class AutomatedTellerMachineController : IAutomatedTellerMachineController
    {
        public void HandleAutomatedTellerMachineMenu()
        {
            // Call GetAllAutomatedTellerMachines() and store the result in a variable
            // Store variable for which atm is selected. The first is selected by default.

            // Show automated teller machine menu and await user input
            // If user input is 1, call UseAutomatedTellerMachine()
            // If user input is 2, call SwitchAutomatedTellerMachine()
            // If user input is 0, return to main menu
        }

        public void CreateAutomatedTellerMachine()
        {
            // Create a new automated teller machine

            // First pick a bank, which owns the machine.
            // Then decide the minimum exchange amount.

            // Create the automated teller machine and save it in the database
        }

        public void DeleteAutomatedTellerMachine()
        {
            // Get all automated teller machines and store them in a variable
            // Select an automated teller machine from the list and delete it
        }

        public void UseAutomatedTellerMachine(AutomatedTellerMachineEntity atm)
        {
            // Use an automated teller machine
        }

        public AutomatedTellerMachineEntity SwitchAutomatedTellerMachine(List<AutomatedTellerMachineEntity> atms)
        {
            // Switch to another automated teller machine
            // Select an atm from the list and return it

            throw new NotImplementedException();
        }

        public List<AutomatedTellerMachineEntity> GetAllAutomatedTellerMachines()
        {
            // Get all automated teller machines and return them

            throw new NotImplementedException();
        }

        public void DepositMoney(AutomatedTellerMachineEntity atm, CreditCardEntity creditCard)
        {
            // Deposit money into the atm
        }

        public void WithdrawMoney(AutomatedTellerMachineEntity atm, CreditCardEntity creditCard)
        {
            // Deposit money into the atm
        }
    }
}