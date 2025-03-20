using Hæveautomaten.Entities;

namespace Hæveautomaten.Interfaces.Repositories
{
    public interface IAutomatedTellerMachineRepository
    {
        bool CreateAutomatedTellerMachine(AutomatedTellerMachineEntity atm);
        bool DeleteAutomatedTellerMachine(uint atmId);
        List<AutomatedTellerMachineEntity> GetAllAutomatedTellerMachines();
    }
}