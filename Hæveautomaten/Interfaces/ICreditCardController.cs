using Hæveautomaten.Entities;

namespace Hæveautomaten.Interfaces
{
    public interface ICreditCardController
    {
        void CreateCreditCard();
        void DeleteCreditCard();
        List<CreditCardEntity> GetAllCreditCards();
        bool IsCreditCardValid(CreditCardEntity creditCard);
    }
}