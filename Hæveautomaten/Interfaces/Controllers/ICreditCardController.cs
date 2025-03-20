using Hæveautomaten.Entities;

namespace Hæveautomaten.Interfaces.Controllers
{
    public interface ICreditCardController
    {
        bool CreateCreditCard(CreditCardEntity creditCard);
        bool DeleteCreditCard(CreditCardEntity creditCard);
        List<CreditCardEntity> GetAllCreditCards();
        bool IsCreditCardValid(CreditCardEntity creditCard);
    }
}