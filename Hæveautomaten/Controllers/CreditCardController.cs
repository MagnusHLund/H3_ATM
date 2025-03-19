using Hæveautomaten.Interfaces;
using Hæveautomaten.Entities;

namespace Hæveautomaten.Controllers
{
    public class CreditCardController : ICreditCardController
    {
        public void CreateCreditCard()
        {
            // Create a new credit card

            // First pick a person, that owns the credit card
            // Get the values of the credit card number, expiration date, and cvv, 
            // Expiration date, pin code, isBlocked, associatedAccountNumber
        }

        public void DeleteCreditCard()
        {
            // Get all credit cards and store them in a variable
            // Select an credit cards from the list and delete it
        }

        public List<CreditCardEntity> GetAllCreditCards()
        {
            // Get all credit cards and return them

            throw new NotImplementedException();
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