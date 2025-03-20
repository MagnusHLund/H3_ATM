using Hæveautomaten.Views;
using Hæveautomaten.Entities;
using Hæveautomaten.Interfaces.Controllers;
using Hæveautomaten.Interfaces.Repositories;

namespace Hæveautomaten.Controllers
{
    public class BankController : IBankController
    {
        private readonly IBankRepository _bankRepository;

        public BankController(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        public bool CreateBank()
        {
            string bankName = CustomView.GetUserInputWithTitle("Enter the bank name: ");

            BankEntity bank = new BankEntity(
                bankName: bankName
            );

            bool success = _bankRepository.CreateBank(bank);
            return success;
        }

        public bool DeleteBank()
        {
            BankEntity bank = SelectBank();

            bool success = _bankRepository.DeleteBank(bank.BankId);
            return success;
        }

        public BankEntity SelectBank()
        {
            List<BankEntity> banks = GetAllBanks();
            string[] bankIdentifiers = banks.Select(bank => bank.ToString()).ToArray();

            CustomView.CustomMenu(bankIdentifiers);

            string userInput = CustomView.GetUserInput();
            int bankIndex = int.Parse(userInput) - 1;

            return banks[bankIndex];
        }

        public List<BankEntity> GetAllBanks()
        {
            List<BankEntity> banks = _bankRepository.GetAllBanks();
            return banks;
        }
    }
}