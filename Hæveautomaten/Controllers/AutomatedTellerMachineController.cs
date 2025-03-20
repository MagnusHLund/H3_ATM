using Hæveautomaten.Entities;
using Hæveautomaten.Interfaces.Controllers;

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

        public bool CreateAutomatedTellerMachine(AutomatedTellerMachineEntity atm)
        {
            // Create the automated teller machine and save it in the database

            throw new NotImplementedException();
        }

        public bool DeleteAutomatedTellerMachine(AutomatedTellerMachineEntity atm)
        {
            // Delete the automated teller machine from the database

            throw new NotImplementedException();
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