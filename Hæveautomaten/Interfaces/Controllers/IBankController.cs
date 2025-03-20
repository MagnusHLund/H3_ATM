using Hæveautomaten.Entities;

namespace Hæveautomaten.Interfaces.Controllers
{
    public interface IBankController
    {
        bool CreateBank();
        bool DeleteBank();
        BankEntity SelectBank();
        List<BankEntity> GetAllBanks();
    }
}