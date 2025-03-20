using Hæveautomaten.Data;
using Hæveautomaten.Entities;
using Microsoft.EntityFrameworkCore;
using Hæveautomaten.Interfaces.Repositories;

namespace Hæveautomaten.Repositories
{
    public class AutomatedTellerMachineRepository : IAutomatedTellerMachineRepository
    {
        private readonly HæveautomatenDbContext _context;

        public AutomatedTellerMachineRepository(HæveautomatenDbContext context)
        {
            _context = context;
        }

        public bool CreateAutomatedTellerMachine(AutomatedTellerMachineEntity atm)
        {
            try
            {
                _context.AutomatedTellerMachines.Add(atm);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating ATM: {ex.Message}");
                return false;
            }
        }

        public bool DeleteAutomatedTellerMachine(uint atmId)
        {
            try
            {
                AutomatedTellerMachineEntity atm = _context.AutomatedTellerMachines.Find(atmId);
                if (atm == null)
                {
                    throw new KeyNotFoundException($"ATM with ID {atmId} not found.");
                }

                _context.AutomatedTellerMachines.Remove(atm);
                _context.SaveChanges();
                return true;
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Error deleting ATM: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting ATM: {ex.Message}");
                return false;
            }
        }

        public List<AutomatedTellerMachineEntity> GetAllAutomatedTellerMachines()
        {
            try
            {
                return _context.AutomatedTellerMachines
                    .Include(atm => atm.Bank) 
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving ATMs: {ex.Message}");
                return new List<AutomatedTellerMachineEntity>();
            }
        }
    }
}