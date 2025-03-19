using Hæveautomaten.Models;

namespace Hæveautomaten.Factories
{
    internal static class CreditCardFactory
    {
        internal static CreditCard CreateValidCreditCard(Person cardHolder, Account associatedAccount)
        {
            string fullCardHolderName = cardHolder.GetFullName();
            ulong cardNumber = 1234123412341234;
            ushort cvv = 123;
            DateTime expirationDate = DateTime.Now.AddYears(3);
            ushort pinCode = 1234;
            bool isBlocked = false;
            ulong associatedAccountNumber = associatedAccount.AccountNumber;

            CreditCard creditCard = new CreditCard
            (
                cardHolderName: fullCardHolderName,
                cardNumber: cardNumber,
                cvv: cvv,
                expirationDate: expirationDate,
                pinCode: pinCode,
                isBlocked: isBlocked,
                associatedAccountNumber: associatedAccountNumber
            );

            cardHolder.AddCreditCard(creditCard);
            associatedAccount.AddCreditCard(creditCard);

            return creditCard;
        }

        internal static CreditCard CreateBlockedCreditCard(Person cardHolder, Account associatedAccount)
        {
            CreditCard creditCard = CreateValidCreditCard(cardHolder, associatedAccount);
            creditCard.IsBlocked = true;

            return creditCard;
        }
    }
}