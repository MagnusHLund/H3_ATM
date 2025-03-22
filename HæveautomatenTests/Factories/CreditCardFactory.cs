using Hæveautomaten.Entities;
using HæveautomatenTests.Utils;

namespace HæveautomatenTests.Factories
{
    internal static class CreditCardFactory
    {
        internal static CreditCardEntity CreateCreditCard()
        {
            return TestDataGeneratorUtils.CreditCardFaker.Generate();
        }

        internal static CreditCardEntity CreateCreditCard(
            PersonEntity cardHolder = null,
            AccountEntity associatedAccount = null,
            string cardNumber = "1234123412341234",
            DateTime? expirationDate = null,
            ushort cvv = 123,
            ushort pinCode = 1234,
            bool isBlocked = false
        )
        {
            cardHolder ??= PersonFactory.CreatePerson();
            associatedAccount ??= AccountFactory.CreateAccount(cardHolder, BankFactory.CreateBank());
            expirationDate ??= DateTime.Now.AddYears(3);

            return new CreditCardEntity(
                cardHolderName: cardHolder.ToString(),
                cardNumber: cardNumber,
                cvv: cvv,
                expirationDate: expirationDate.Value,
                pinCode: pinCode,
                isBlocked: isBlocked,
                account: associatedAccount
            );
        }
    }
}