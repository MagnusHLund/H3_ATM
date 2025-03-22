using Hæveautomaten.Entities;
using HæveautomatenTests.Utils;

namespace HæveautomatenTests.Factories
{
    internal static class BankFactory
    {
        internal static BankEntity CreateBank()
        {
            return TestDataGeneratorUtils.BankFaker.Generate();
        }

        internal static BankEntity CreateBank(string bankName = "Bank of Bankington")
        {
            return new BankEntity(
                bankName: bankName
            );
        }
    }
}