using Hæveautomaten.Models;

namespace HæveautomatenTests.Factories
{
    internal static class AddressFactory
    {
        internal static Address CreateAddress()
        {
            string houseNumber = "1B";
            string street = "Banking street";
            string city = "Bankington";
            string zipCode = "1234";

            return new Address(
                houseNumber: houseNumber,
                street: street,
                city: city,
                zipCode: zipCode
            );
        }
    }
}