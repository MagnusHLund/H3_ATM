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
            try
            {
                _context.CreditCards.Add(creditCard);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating credit card: {ex.Message}");
                return false;
            }
        }

        public bool DeleteCreditCard(uint creditCardId)
        {
            try
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
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Error deleting credit card: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting credit card: {ex.Message}");
                return false;
            }
        }

        public List<CreditCardEntity> GetAllCreditCards()
        {
            try
            {
                return _context.CreditCards
                    .Include(cc => cc.Account)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving credit cards: {ex.Message}");
                return new List<CreditCardEntity>();
            }
        }
    }
}