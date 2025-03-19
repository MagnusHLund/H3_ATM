namespace Hæveautomaten.Models
{
	public class AutomatedTellerMachine
	{
		public uint MinimumExchangeAmount { get; set; }

		public AutomatedTellerMachine(uint minimumExchangeAmount)
		{
			MinimumExchangeAmount = minimumExchangeAmount;
		}

		public void DepositMoney(Account account, uint amount)
		{
			EnsureExchangeAmountIsValid(amount);

			account.Balance += amount;
		}

		public void WithdrawMoney(Account account, uint amount)
		{
			EnsureExchangeAmountIsValid(amount);

			if (account.Balance < amount)
			{
				throw new InvalidOperationException("Insufficient funds");
			}

			account.Balance -= amount;
		}

		private void EnsureExchangeAmountIsValid(uint amount)
		{
			if (amount < MinimumExchangeAmount)
			{
				throw new InvalidOperationException("Amount is less than minimum exchange amount");
			}
		}
	}
}
