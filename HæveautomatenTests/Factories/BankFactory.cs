using Hæveautomaten.Models;

namespace HæveautomatenTests.Factories
{
    internal static class BankFactory
    {
        internal static Bank CreateBank()
        {
            string bankName = "Bank of Bankington";
            Address address = AddressFactory.CreateAddress();

            return new Bank(
                bankName: bankName,
                bankAddress: address
            );
        }
    }
}