using Hæveautomaten.Entities;

namespace HæveautomatenTests.Factories
{
    internal static class AccountFactory
    {
        internal static AccountEntity CreateAccount(
            PersonEntity accountOwner = null,
            BankEntity associatedBank = null,
            uint accountId = 1234567890,
            long balanceInMinorUnits = 1000
        )
        {
            accountOwner ??= PersonFactory.CreatePerson();
            associatedBank ??= BankFactory.CreateBank();

            string accountOwnerFullName = accountOwner.GetFullName();

            AccountEntity account = new AccountEntity
            (
                accountId: accountId,
                accountOwnerName: accountOwnerFullName,
                balanceInMinorUnits: balanceInMinorUnits,
                bank: associatedBank
            );

            associatedBank.Accounts.Add(account);

            return account;
        }
    }
}