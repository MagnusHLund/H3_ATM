using Hæveautomaten.Models;

namespace Hæveautomaten.Interfaces
{
    public interface ICardController
    {
        bool IsCreditCardValid(CreditCard creditCard);
    }
}