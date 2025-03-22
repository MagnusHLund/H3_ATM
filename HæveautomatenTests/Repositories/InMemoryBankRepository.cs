using Hæveautomaten.Entities;
using Hæveautomaten.Interfaces.Repositories;

namespace HæveautomatenTests.Repositories
{
    public class InMemoryBankRepository : IBankRepository
    {
        private readonly List<BankEntity> _banks = new List<BankEntity>();

        public bool CreateBank(BankEntity bank)
        {
            if (_banks.Any(b => b.BankName == bank.BankName))
            {
                throw new ArgumentException($"A bank with the name '{bank.BankName}' already exists.");
            }

            _banks.Add(bank);
            return true;
        }

        public bool DeleteBank(int bankId)
        {
            BankEntity bank = _banks.FirstOrDefault(b => b.BankId == bankId);

            if (bank == null)
            {
                throw new KeyNotFoundException($"Bank with ID {bankId} not found.");
            }

            _banks.Remove(bank);
            return true;
        }

        public List<BankEntity> GetAllBanks()
        {
            return new List<BankEntity>(_banks);
        }
    }
}