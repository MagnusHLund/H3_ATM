
using Bogus;
using Hæveautomaten.Entities;

namespace HæveautomatenTests.Utils
{
    public static class TestDataGeneratorUtils
    {
        private static int _personIdCounter = 1;
        private static int _bankIdCounter = 1;
        private static int _accountIdCounter = 1;
        private static int _creditCardIdCounter = 1;
        private static int _atmIdCounter = 1;

        public static Faker<PersonEntity> PersonFaker => new Faker<PersonEntity>()
            .RuleFor(p => p.PersonId, f => _personIdCounter++)
            .RuleFor(p => p.FirstName, f => f.Name.FirstName())
            .RuleFor(p => p.LastName, f => f.Name.LastName());

        public static Faker<BankEntity> BankFaker => new Faker<BankEntity>()
            .RuleFor(b => b.BankId, f => _bankIdCounter++)
            .RuleFor(b => b.BankName, f => f.Company.CompanyName());

        public static Faker<AccountEntity> AccountFaker => new Faker<AccountEntity>()
            .RuleFor(a => a.AccountId, f => _accountIdCounter++)
            .RuleFor(a => a.BalanceInMinorUnits, f => f.Random.Long(0, 1000000))
            .RuleFor(a => a.AccountOwner, f => PersonFaker.Generate())
            .RuleFor(a => a.Bank, f => BankFaker.Generate());

        public static Faker<CreditCardEntity> CreditCardFaker => new Faker<CreditCardEntity>()
            .RuleFor(c => c.CreditCardId, f => _creditCardIdCounter++)
            .RuleFor(c => c.CardNumber, f => f.Finance.CreditCardNumber().Replace("-", "")) // Keep as string
            .RuleFor(c => c.ExpirationDate, f => f.Date.Future())
            .RuleFor(c => c.Cvv, f => f.Random.UShort(100, 999))
            .RuleFor(c => c.PinCode, f => f.Random.UShort(1000, 9999))
            .RuleFor(c => c.IsBlocked, f => f.Random.Bool())
            .RuleFor(c => c.Account, f => AccountFaker.Generate())
            .RuleFor(c => c.CardHolderName, f => f.Person.FullName);

        public static Faker<AutomatedTellerMachineEntity> AtmFaker => new Faker<AutomatedTellerMachineEntity>()
            .RuleFor(atm => atm.AutomatedTellerMachineId, f => _atmIdCounter++)
            .RuleFor(atm => atm.MinimumExchangeAmount, f => f.Random.UInt(50, 500))
            .RuleFor(atm => atm.Bank, f => BankFaker.Generate());
    }
}