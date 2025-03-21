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
            _context.AutomatedTellerMachines.Add(atm);
            _context.SaveChanges();
            return true;

        }

        public bool DeleteAutomatedTellerMachine(int atmId)
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

        public List<AutomatedTellerMachineEntity> GetAllAutomatedTellerMachines()
        {
            List<AutomatedTellerMachineEntity> atms = _context.AutomatedTellerMachines
                .Include(atm => atm.Bank)
                .ToList();

            return atms;
        }
    }
}