using Hæveautomaten.Entities;
using Hæveautomaten.Interfaces.Views;
using Hæveautomaten.Interfaces.Controllers;
using Hæveautomaten.Interfaces.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Hæveautomaten.Controllers
{
    public class CreditCardController : ICreditCardController
    {
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly IPersonController _personController;
        private readonly IAccountController _accountController;
        private readonly IBaseView _baseView;

        public CreditCardController(
            ICreditCardRepository creditCardRepository,
            IPersonController personController,
            IAccountController accountController,
            IBaseView baseView
        )
        {
            _creditCardRepository = creditCardRepository;
            _personController = personController;
            _accountController = accountController;
            _baseView = baseView;
        }

        public bool CreateCreditCard()
        {
            PersonEntity cardOwner = _personController.SelectPerson();
            string cardHolderName = cardOwner.ToString();

            List<AccountEntity> accountsByPerson = _accountController.GetAccountsByPerson(cardOwner);
            AccountEntity associatedAccount = _accountController.SelectAccount(accountsByPerson);

            string cardNumber = _baseView.GetUserInputWithTitle("Enter the card number: ");
            string stringExpirationDate = _baseView.GetUserInputWithTitle("Enter the expiration date (MM/YY): ");
            ushort cvv = ushort.Parse(_baseView.GetUserInputWithTitle("Enter the CVV: "));
            ushort pinCode = ushort.Parse(_baseView.GetUserInputWithTitle("Enter the pin code: "));
            bool isBlocked = _baseView.GetUserInputWithTitle("Is the card blocked? (true/false): ") == "true";

            DateTime expirationDate = ConvertStringToDateTime(stringExpirationDate);

            CreditCardEntity creditCard = new CreditCardEntity(
                cardHolderName: cardHolderName,
                cardNumber: cardNumber,
                expirationDate: expirationDate,
                cvv: cvv,
                pinCode: pinCode,
                isBlocked: isBlocked,
                account: associatedAccount
            );

            if (!IsCreditCardValid(creditCard))
            {
                throw new ValidationException("Invalid credit card information!");
            }

            bool success = _creditCardRepository.CreateCreditCard(creditCard);
            return success;
        }

        public bool DeleteCreditCard()
        {
            CreditCardEntity creditCard = SelectCreditCard();

            bool success = _creditCardRepository.DeleteCreditCard(creditCard.CreditCardId);
            return success;
        }

        public CreditCardEntity SelectCreditCard()
        {
            List<CreditCardEntity> creditCards = GetAllCreditCards();
            string[] CreditCardIdentifiers = creditCards.Select(creditCard => creditCard.ToString()).ToArray();

            _baseView.CustomMenu(CreditCardIdentifiers);

            string userInput = _baseView.GetUserInput();
            int creditCardIndex = int.Parse(userInput) - 1;

            return creditCards[creditCardIndex];
        }

        public List<CreditCardEntity> GetAllCreditCards()
        {
            List<CreditCardEntity> creditCards = _creditCardRepository.GetAllCreditCards();
            return creditCards;
        }

        public bool IsCreditCardValid(CreditCardEntity creditCard)
        {
            if (creditCard == null)
            {
                return false;
            }

            if (creditCard.CardNumber.Length != 16 || long.Parse(creditCard.CardNumber) < 0)
            {
                return false;
            }

            if (creditCard.ExpirationDate < DateTime.Now)
            {
                return false;
            }

            if (creditCard.Cvv < 100 || creditCard.Cvv > 9999)
            {
                return false;
            }

            if (creditCard.PinCode < 1000 || creditCard.PinCode > 9999)
            {
                return false;
            }

            if (creditCard.Account == null)
            {
                return false;
            }

            return true;
        }

        public DateTime ConvertStringToDateTime(string expirationDate)
        {
            string[] dateParts = expirationDate.Split('/');
            int month = int.Parse(dateParts[0]);
            int year = int.Parse(dateParts[1]) + 2000;

            return new DateTime(year, month, 1);
        }
    }
}