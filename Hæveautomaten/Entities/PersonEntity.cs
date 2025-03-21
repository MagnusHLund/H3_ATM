using System.ComponentModel.DataAnnotations;

namespace Hæveautomaten.Entities
{
    public class PersonEntity
    {
        [Key]
        public int PersonId { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        // Navigation property 
        public List<AccountEntity> Accounts { get; set; } = new List<AccountEntity>();

        public PersonEntity(string firstName, string lastName, string? middleName = null)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }

        private PersonEntity() { }

        public override string ToString()
        {
            if (FirstName == null || LastName == null)
            {
                throw new NullReferenceException("First name and last name must not be null");
            }

            if (FirstName == "" || LastName == "")
            {
                throw new ArgumentException("First name and last name must not be empty");
            }

            return string.IsNullOrEmpty(MiddleName)
                ? $"{FirstName} {LastName}"
                : $"{FirstName} {MiddleName} {LastName}";
        }
    }
}