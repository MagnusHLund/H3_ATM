using System.ComponentModel.DataAnnotations;

namespace Hæveautomaten.Entities
{
    public class AccountEntity
    {
        [Key]
        public uint AccountId { get; set; }

        public long BalanceInMinorUnits { get; set; } = 0;

        // Navigation property
        public List<CreditCardEntity> CreditCards { get; set; } = new List<CreditCardEntity>();

        // Navigation property
        public BankEntity Bank { get; set; }

        // Navigation property
        public PersonEntity AccountOwner { get; set; }

        public AccountEntity(long balanceInMinorUnits = 0, BankEntity bank = null, PersonEntity accountOwner = null, List<CreditCardEntity> creditCards = null)
        {
            BalanceInMinorUnits = balanceInMinorUnits;
            Bank = bank;
            CreditCards = creditCards ?? new List<CreditCardEntity>();
            AccountOwner = accountOwner;
        }

        private AccountEntity() { }

        public override string ToString()
        {
            return $"Account ID: {AccountId} - Balance: {BalanceInMinorUnits / 100} DKK";
        }
    }
}