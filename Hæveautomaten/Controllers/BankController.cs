using Hæveautomaten.Entities;
using Hæveautomaten.Interfaces.Controllers;

namespace Hæveautomaten.Controllers
{
    public class BankController : IBankController
    {
        public bool CreateBank(BankEntity bank)
        {
            // Create bank and save it in the database

            throw new NotImplementedException();
        }

        public bool DeleteBank(BankEntity bank)
        {
            // Delete the bank from the database

            throw new NotImplementedException();
        }

        public List<BankEntity> GetAllBanks()
        {
            // Get all banks and return them

            throw new NotImplementedException();
        }
    }
}