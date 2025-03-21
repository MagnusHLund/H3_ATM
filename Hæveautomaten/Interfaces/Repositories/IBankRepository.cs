using Hæveautomaten.Entities;

namespace Hæveautomaten.Interfaces.Repositories
{
    public interface IBankRepository
    {
        bool CreateBank(BankEntity bank);
        bool DeleteBank(int bankId);
        List<BankEntity> GetAllBanks();
    }
}