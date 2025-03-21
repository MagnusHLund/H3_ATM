using Hæveautomaten.Entities;
using Hæveautomaten.Interfaces.Views;
using Hæveautomaten.Interfaces.Controllers;
using Hæveautomaten.Interfaces.Repositories;

namespace Hæveautomaten.Controllers
{
    public class BankController : IBankController
    {
        private readonly IBankRepository _bankRepository;
        private readonly IBaseView _baseView;

        public BankController(
            IBankRepository bankRepository,
            IBaseView baseView
        )
        {
            _bankRepository = bankRepository;
            _baseView = baseView;
        }

        public bool CreateBank()
        {
            string bankName = _baseView.GetUserInputWithTitle("Enter the bank name: ");

            BankEntity bank = new BankEntity(
                bankName: bankName
            );

            if (bank.BankName == null)
            {
                throw new ArgumentNullException("Bank name cannot be null.");
            }

            if (bank.BankName == "")
            {
                throw new ArgumentException("Bank name cannot be empty.");
            }

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

            _baseView.CustomMenu(bankIdentifiers);

            string userInput = _baseView.GetUserInput();
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