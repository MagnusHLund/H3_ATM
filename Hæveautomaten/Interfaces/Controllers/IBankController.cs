using Hæveautomaten.Entities;

namespace Hæveautomaten.Interfaces.Controllers
{
    public interface IBankController
    {
        bool CreateBank(BankEntity bank);
        bool DeleteBank(BankEntity bank);
        List<BankEntity> GetAllBanks();
    }
}