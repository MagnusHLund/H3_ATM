using Hæveautomaten.Entities;
using Hæveautomaten.Interfaces.Repositories;

namespace HæveautomatenTests.Repositories
{
    public class InMemoryAutomatedTellerMachineRepository : IAutomatedTellerMachineRepository
    {
        private readonly List<AutomatedTellerMachineEntity> _atms = new List<AutomatedTellerMachineEntity>();

        public bool CreateAutomatedTellerMachine(AutomatedTellerMachineEntity atm)
        {
            _atms.Add(atm);
            return true;
        }

        public bool DeleteAutomatedTellerMachine(int atmId)
        {
            var atm = _atms.FirstOrDefault(a => a.AutomatedTellerMachineId == atmId);
            if (atm != null)
            {
                _atms.Remove(atm);
                return true;
            }
            return false;
        }

        public List<AutomatedTellerMachineEntity> GetAllAutomatedTellerMachines()
        {
            return new List<AutomatedTellerMachineEntity>(_atms);
        }
    }
}