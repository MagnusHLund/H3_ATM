using Hæveautomaten.Models;

namespace HæveautomatenTests.Factories
{
    internal static class PersonFactory
    {
        internal static Person CreatePerson()
        {
            string firstName = "John";
            string middleName = "James";
            string lastName = "Doe";

            return new Person(
                firstName: firstName,
                middleName: middleName,
                lastName: lastName
            );
        }
    }
}