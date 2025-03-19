namespace Hæveautomaten.Models
{
    public class Bank
    {
        public string BankName { get; set; }
        public Address BankAddress { get; set; }
        public List<Account> Accounts { get; set; } = new List<Account>();
        public List<AutomatedTellerMachine> AutomatedTellerMachines { get; set; } = new List<AutomatedTellerMachine>();

        public Bank(string bankName, Address bankAddress)
        {
            BankName = bankName;
            BankAddress = bankAddress;
        }

        public void AddAccount(Account account)
        {
            Accounts.Add(account);
        }

        public void AddAutomatedTellerMachine(AutomatedTellerMachine automatedTellerMachine)
        {
            AutomatedTellerMachines.Add(automatedTellerMachine);
        }
    }
}