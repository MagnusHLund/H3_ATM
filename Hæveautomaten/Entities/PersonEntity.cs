using System.ComponentModel.DataAnnotations;

namespace Hæveautomaten.Entities
{
    public class PersonEntity
    {
        [Key]
        public uint PersonId { get; set; }

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

        public string GetFullName()
        {
            return MiddleName == null
                ? $"{FirstName} {LastName}"
                : $"{FirstName} {MiddleName} {LastName}";
        }
    }
}