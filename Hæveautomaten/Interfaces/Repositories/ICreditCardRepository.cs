using Hæveautomaten.Entities;

namespace Hæveautomaten.Interfaces.Repositories
{
    public interface ICreditCardRepository
    {
        bool CreateCreditCard(CreditCardEntity creditCard);
        bool DeleteCreditCard(int creditCardId);
        List<CreditCardEntity> GetAllCreditCards();
    }
}