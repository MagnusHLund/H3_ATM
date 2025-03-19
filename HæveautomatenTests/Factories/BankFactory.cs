using Hæveautomaten.Entities;

namespace HæveautomatenTests.Factories
{
    internal static class BankFactory
    {
        internal static BankEntity CreateBank(string bankName = "Bank of Bankington")
        {
            return new BankEntity(
                bankName: bankName
            );
        }
    }
}