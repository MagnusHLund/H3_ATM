using Hæveautomaten.Data;
using Hæveautomaten.Entities;
using Microsoft.EntityFrameworkCore;
using Hæveautomaten.Interfaces.Repositories;

namespace Hæveautomaten.Repositories
{
    public class BankRepository : IBankRepository
    {
        private readonly HæveautomatenDbContext _context;

        public BankRepository(HæveautomatenDbContext context)
        {
            _context = context;
        }

        public bool CreateBank(BankEntity bank)
        {
            _context.Banks.Add(bank);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteBank(int bankId)
        {
            BankEntity bank = _context.Banks.Find(bankId);

            if (bank == null)
            {
                throw new KeyNotFoundException($"Bank with ID {bankId} not found.");
            }

            _context.Banks.Remove(bank);
            _context.SaveChanges();
            return true;
        }

        public List<BankEntity> GetAllBanks()
        {
            List<BankEntity> banks = _context.Banks
                .Include(b => b.Accounts)
                .Include(b => b.AutomatedTellerMachines)
                .ToList();

            return banks;
        }
    }
}