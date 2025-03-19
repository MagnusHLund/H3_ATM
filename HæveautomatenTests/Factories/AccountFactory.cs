using Hæveautomaten.Models;

namespace Hæveautomaten.Factories
{
    internal static class AccountFactory
    {
        internal static Account CreateAccount(Person accountOwner, Bank associatedBank)
        {
            string accountOwnerFullName = accountOwner.GetFullName();
            uint accountNumber = 1234567890;
            decimal balance = 1000;

            Account account = new Account(
                accountOwnerName: accountOwnerFullName,
                accountNumber: accountNumber,
                balance: balance
            );

            associatedBank.AddAccount(account);

            return account;
        }
    }
}