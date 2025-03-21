using System.ComponentModel.DataAnnotations;

namespace Hæveautomaten.Entities
{
    public class CreditCardEntity
    {
        [Key]
        public int CreditCardId { get; set; }

        [Required]
        public ulong CardNumber { get; set; }

        [Required]
        public string CardHolderName { get; set; }

        [Required]
        public ushort Cvv { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public ushort PinCode { get; set; }

        public bool IsBlocked { get; set; }

        // Navigation property 
        public AccountEntity Account { get; set; }

        public CreditCardEntity(ulong cardNumber, string cardHolderName, ushort cvv, DateTime expirationDate, ushort pinCode, bool isBlocked, AccountEntity account)
        {
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            Cvv = cvv;
            ExpirationDate = expirationDate;
            PinCode = pinCode;
            IsBlocked = isBlocked;
            Account = account;
        }

        private CreditCardEntity() { }

        public override string ToString()
        {
            return $"{CardHolderName} - {CardNumber}";
        }
    }
}