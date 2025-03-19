namespace Hæveautomaten.Models
{
    public class Person
    {
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public List<CreditCard> creditCards { get; set; } = new List<CreditCard>();

        public Person(string firstName, string lastName, string? middleName)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
        }

        public void AddCreditCard(CreditCard creditCard)
        {
            creditCards.Add(creditCard);
        }

        public string GetFullName()
        {
            if (MiddleName == null)
            {
                return $"{FirstName} {LastName}";
            }

            return $"{FirstName} {MiddleName} {LastName}";
        }
    }
}