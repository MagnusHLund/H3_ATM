using Hæveautomaten.Entities;

namespace HæveautomatenTests.Factories
{
    internal static class PersonFactory
    {
        internal static PersonEntity CreatePerson(
            string firstName = "John",
            string middleName = "James",
            string lastName = "Doe"
        )
        {
            return new PersonEntity(
                firstName: firstName,
                middleName: middleName,
                lastName: lastName
            );
        }
    }
}