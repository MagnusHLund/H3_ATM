using Hæveautomaten.Data;
using Hæveautomaten.Entities;
using Microsoft.EntityFrameworkCore;
using Hæveautomaten.Interfaces.Repositories;

namespace Hæveautomaten.Repositories
{
    public class CreditCardRepository : ICreditCardRepository
    {
        private readonly HæveautomatenDbContext _context;

        public CreditCardRepository(HæveautomatenDbContext context)
        {
            _context = context;
        }

        public bool CreateCreditCard(CreditCardEntity creditCard)
        {
            _context.CreditCards.Add(creditCard);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteCreditCard(int creditCardId)
        {
            CreditCardEntity creditCard = _context.CreditCards.Find(creditCardId);

            if (creditCard == null)
            {
                throw new KeyNotFoundException($"Credit card with ID {creditCardId} not found.");
            }

            _context.CreditCards.Remove(creditCard);
            _context.SaveChanges();
            return true;
        }

        public List<CreditCardEntity> GetAllCreditCards()
        {
            List<CreditCardEntity> creditCards = _context.CreditCards
                .Include(cc => cc.Account)
                .ToList();

            return creditCards;
        }
    }
}