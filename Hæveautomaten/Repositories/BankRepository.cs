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
            try
            {
                _context.Banks.Add(bank);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating bank: {ex.Message}");
                return false;
            }
        }

        public bool DeleteBank(uint bankId)
        {
            try
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
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Error deleting bank: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting bank: {ex.Message}");
                return false;
            }
        }

        public List<BankEntity> GetAllBanks()
        {
            try
            {
                return _context.Banks
                    .Include(b => b.Accounts)
                    .Include(b => b.AutomatedTellerMachines)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving banks: {ex.Message}");
                return new List<BankEntity>();
            }
        }
    }
}