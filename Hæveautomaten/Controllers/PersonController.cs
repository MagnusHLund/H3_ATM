using Hæveautomaten.Entities;
using Hæveautomaten.Interfaces.Controllers;
using Hæveautomaten.Interfaces.Repositories;
using Hæveautomaten.Interfaces.Views;

namespace Hæveautomaten.Controllers
{
    public class PersonController : IPersonController
    {
        private readonly IPersonRepository _personRepository;
        private readonly IBaseView _baseView;

        public PersonController(
            IPersonRepository personRepository,
            IBaseView baseView
        )
        {
            _personRepository = personRepository;
            _baseView = baseView;
        }

        public bool CreatePerson()
        {
            string firstName = _baseView.GetUserInputWithTitle("Enter the first name: ");
            string lastName = _baseView.GetUserInputWithTitle("Enter the last name: ");
            string middleName = _baseView.GetUserInputWithTitle("Enter the middle name: ");

            PersonEntity person = new PersonEntity
            (
                firstName: firstName,
                middleName: middleName,
                lastName: lastName
            );

            bool success = _personRepository.CreatePerson(person);
            return success;
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

            _baseView.CustomMenu(peopleNames);

            string userInput = _baseView.GetUserInput();
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