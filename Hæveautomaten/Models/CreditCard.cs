namespace Hæveautomaten.Models
{
	public class CreditCard
	{
		public string CardHolderName { get; set; }
		public ulong CardNumber { get; set; }
		public ushort Cvv { get; set; }
		public DateTime ExpirationDate { get; set; }
		public ushort PinCode { get; set; }
		public bool IsBlocked { get; set; }
		public ulong AssociatedAccountNumber { get; set; }

		public CreditCard(string cardHolderName, ulong cardNumber, ushort cvv, DateTime expirationDate, ushort pinCode, bool isBlocked, ulong associatedAccountNumber)
		{
			CardHolderName = cardHolderName;
			CardNumber = cardNumber;
			Cvv = cvv;
			ExpirationDate = expirationDate;
			PinCode = pinCode;
			IsBlocked = isBlocked;
			AssociatedAccountNumber = associatedAccountNumber;
		}
	}
}
