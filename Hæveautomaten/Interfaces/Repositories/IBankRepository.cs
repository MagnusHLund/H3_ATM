using Hæveautomaten.Entities;

namespace Hæveautomaten.Interfaces.Repositories
{
    public interface IBankRepository
    {
        bool CreateBank(BankEntity bank);
        bool DeleteBank(uint bankId);
    }
}