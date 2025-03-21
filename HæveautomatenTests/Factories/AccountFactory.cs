using Hæveautomaten.Entities;

namespace HæveautomatenTests.Factories
{
    internal static class AccountFactory
    {
        internal static AccountEntity CreateAccount(
            PersonEntity accountOwner = null,
            BankEntity associatedBank = null,
            List<CreditCardEntity> creditCards = null,
            long balanceInMinorUnits = 1000
        )
        {
            accountOwner ??= PersonFactory.CreatePerson();
            associatedBank ??= BankFactory.CreateBank();
            creditCards ??= new List<CreditCardEntity>();

            AccountEntity account = new AccountEntity
            (
                accountOwner: accountOwner,
                balanceInMinorUnits: balanceInMinorUnits,
                bank: associatedBank,
                creditCards: creditCards
            );

            associatedBank.Accounts.Add(account);

            return account;
        }

        internal static List<AccountEntity> CreateAccounts(
            int numberOfAccounts = 3,
            PersonEntity accountOwner = null,
            BankEntity associatedBank = null,
            List<CreditCardEntity> creditCards = null,
            long balanceInMinorUnits = 1000
        )
        {
            List<AccountEntity> accounts = new List<AccountEntity>();

            for (int i = 0; i < numberOfAccounts; i++)
            {
                accounts.Add(CreateAccount(
                    accountOwner: accountOwner,
                    associatedBank: associatedBank,
                    creditCards: creditCards,
                    balanceInMinorUnits: balanceInMinorUnits
                ));
            }

            return accounts;
        }
    }
}