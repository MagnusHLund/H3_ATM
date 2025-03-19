namespace Hæveautomaten.Models
{
	public class Account
	{
		public string AccountOwnerName { get; set; }
		public ulong AccountNumber { get; set; }
		public decimal Balance { get; set; }
		public List<CreditCard> CreditCards { get; set; } = new List<CreditCard>();

		public Account(string accountOwnerName, ulong accountNumber, decimal balance)
		{
			AccountOwnerName = accountOwnerName;
			AccountNumber = accountNumber;
			Balance = balance;
		}

		public void AddCreditCard(CreditCard creditCard)
		{
			CreditCards.Add(creditCard);
		}
	}
}
