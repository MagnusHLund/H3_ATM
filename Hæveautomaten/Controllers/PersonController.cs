using Hæveautomaten.Views;
using Hæveautomaten.Entities;
using Hæveautomaten.Interfaces.Controllers;
using Hæveautomaten.Interfaces.Repositories;

namespace Hæveautomaten.Controllers
{
    public class PersonController : IPersonController
    {
        private readonly IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public bool CreatePerson()
        {
            string firstName = CustomView.GetUserInputWithTitle("Enter the first name: ");
            string lastName = CustomView.GetUserInputWithTitle("Enter the last name: ");
            string middleName = CustomView.GetUserInputWithTitle("Enter the middle name: ");

            PersonEntity person = new PersonEntity
            (
                firstName: firstName,
                middleName: middleName,
                lastName: lastName
            );

            _personRepository.CreatePerson(person);

            throw new NotImplementedException();
        }

        public bool DeletePerson()
        {
            PersonEntity person = SelectPerson();

            bool success = _personRepository.DeletePerson(person.PersonId);
            return success;
        }

        public PersonEntity SelectPerson()
        {
            List<PersonEntity> people = GetAllPeople();
            string[] peopleNames = people.Select(person => person.ToString()).ToArray();

            CustomView.CustomMenu(peopleNames);

            string userInput = CustomView.GetUserInput();
            int personIndex = int.Parse(userInput) - 1;

            return people[personIndex];
        }

        public List<PersonEntity> GetAllPeople()
        {
            List<PersonEntity> people = _personRepository.GetAllPeople();
            return people;
        }
    }
}