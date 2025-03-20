using Hæveautomaten.Views;
using Hæveautomaten.Entities;
using Hæveautomaten.Interfaces.Controllers;
using Hæveautomaten.Interfaces.Repositories;

namespace Hæveautomaten.Controllers
{
    public class CreditCardController : ICreditCardController
    {
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly IPersonController _personController;
        private readonly IAccountController _accountController;

        public CreditCardController(ICreditCardRepository creditCardRepository, IPersonController personController, IAccountController accountController)
        {
            _creditCardRepository = creditCardRepository;
            _personController = personController;
            _accountController = accountController;
        }

        public bool CreateCreditCard()
        {
            PersonEntity cardOwner = _personController.SelectPerson();
            string cardHolderName = cardOwner.ToString();

            List<AccountEntity> accountsByPerson = _accountController.GetAccountsByPerson(cardOwner);
            AccountEntity associatedAccount = _accountController.SelectAccount(accountsByPerson);

            ulong cardNumber = ulong.Parse(CustomView.GetUserInputWithTitle("Enter the card number: "));
            DateTime expirationDate = DateTime.Parse(CustomView.GetUserInputWithTitle("Enter the expiration date (MM/YY): "));
            ushort cvv = ushort.Parse(CustomView.GetUserInputWithTitle("Enter the CVV: "));
            ushort pinCode = ushort.Parse(CustomView.GetUserInputWithTitle("Enter the pin code: "));
            bool isBlocked = CustomView.GetUserInputWithTitle("Is the card blocked? (true/false): ") == "true";

            CreditCardEntity creditCard = new CreditCardEntity(
                cardHolderName: cardHolderName,
                cardNumber: cardNumber,
                expirationDate: expirationDate,
                cvv: cvv,
                pinCode: pinCode,
                isBlocked: isBlocked,
                account: associatedAccount
            );

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

            CustomView.CustomMenu(CreditCardIdentifiers);

            string userInput = CustomView.GetUserInput();
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
            // Check if the credit card is blocked
            // Check if the credit card number is valid, using Luhn's algorithm
            // Check if the expiration date is valid
            // Check if the cvv is valid
            // Check if the pin code is valid
            // Check if the associated account number is valid

            throw new NotImplementedException();
        }
    }
}