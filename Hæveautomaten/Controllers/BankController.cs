using Hæveautomaten.Interfaces;

namespace Hæveautomaten.Controllers
{
    public class BankController : IBankController
    {
        public void CreateBank()
        {
            // Create a new bank. Input bank name.
            // Create bank and save it in the database
        }

        public void DeleteBank()
        {
            // Get all banks and store them in a variable
            // Select a bank from the list and delete it
        }

        public void GetAllBanks()
        {
            // Get all banks and return them
        }
    }
}