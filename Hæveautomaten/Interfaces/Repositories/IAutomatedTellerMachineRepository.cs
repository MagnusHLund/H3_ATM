using Hæveautomaten.Entities;

namespace Hæveautomaten.Interfaces.Repositories
{
    public interface IAutomatedTellerMachineRepository
    {
        bool CreateAutomatedTellerMachine(AutomatedTellerMachineEntity atm);
        bool DeleteAutomatedTellerMachine(int atmId);
        List<AutomatedTellerMachineEntity> GetAllAutomatedTellerMachines();
    }
}