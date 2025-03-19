using System.ComponentModel.DataAnnotations;

namespace Hæveautomaten.Entities
{
    public class AccountEntity
    {
        [Key]
        public uint AccountId { get; set; }

        [Required]
        public string AccountOwnerName { get; set; }

        public long BalanceInMinorUnits { get; set; } = 0;

        // Navigation property
        public List<CreditCardEntity> CreditCards { get; set; } = new List<CreditCardEntity>();

        // Navigation property
        public BankEntity Bank { get; set; }

        public AccountEntity(uint accountId, string accountOwnerName, long balanceInMinorUnits = 0, BankEntity bank = null, List<CreditCardEntity> creditCards = null)
        {
            AccountId = accountId;
            AccountOwnerName = accountOwnerName;
            BalanceInMinorUnits = balanceInMinorUnits;
            Bank = bank;
            CreditCards = creditCards ?? new List<CreditCardEntity>();
        }
    }
}