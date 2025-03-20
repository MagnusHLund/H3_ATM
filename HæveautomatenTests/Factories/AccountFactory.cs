using Hæveautomaten.Entities;

namespace HæveautomatenTests.Factories
{
    internal static class AccountFactory
    {
        internal static AccountEntity CreateAccount(
            PersonEntity accountOwner = null,
            BankEntity associatedBank = null,
            long balanceInMinorUnits = 1000
        )
        {
            accountOwner ??= PersonFactory.CreatePerson();
            associatedBank ??= BankFactory.CreateBank();

            AccountEntity account = new AccountEntity
            (
                accountOwner: accountOwner,
                balanceInMinorUnits: balanceInMinorUnits,
                bank: associatedBank
            );

            associatedBank.Accounts.Add(account);

            return account;
        }

        internal static List<AccountEntity> CreateAccounts(
            int numberOfAccounts = 3,
            PersonEntity accountOwner = null,
            BankEntity associatedBank = null,
            long balanceInMinorUnits = 1000
        )
        {
            List<AccountEntity> accounts = new List<AccountEntity>();

            for (int i = 0; i < numberOfAccounts; i++)
            {
                accounts.Add(CreateAccount(accountOwner, associatedBank, balanceInMinorUnits));
            }

            return accounts;
        }
    }
}