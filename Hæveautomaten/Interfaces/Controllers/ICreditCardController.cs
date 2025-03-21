using Hæveautomaten.Entities;

namespace Hæveautomaten.Interfaces.Controllers
{
    public interface ICreditCardController
    {
        bool CreateCreditCard();
        bool DeleteCreditCard();
        CreditCardEntity SelectCreditCard();
        List<CreditCardEntity> GetAllCreditCards();
        bool IsCreditCardValid(CreditCardEntity creditCard);
        DateTime ConvertStringToDateTime(string expirationDate);
    }
}