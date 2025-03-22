using Hæveautomaten.Entities;
using Hæveautomaten.Interfaces.Repositories;

namespace HæveautomatenTests.Repositories
{
    public class InMemoryCreditCardRepository : ICreditCardRepository
    {
        private readonly List<CreditCardEntity> _creditCards = new List<CreditCardEntity>();

        public bool CreateCreditCard(CreditCardEntity creditCard)
        {
            _creditCards.Add(creditCard);
            return true;
        }

        public bool DeleteCreditCard(int creditCardId)
        {
            var creditCard = _creditCards.FirstOrDefault(c => c.CreditCardId == creditCardId);
            if (creditCard != null)
            {
                _creditCards.Remove(creditCard);
                return true;
            }
            return false;
        }

        public List<CreditCardEntity> GetAllCreditCards()
        {
            return new List<CreditCardEntity>(_creditCards);
        }
    }
}