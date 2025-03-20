using System.ComponentModel.DataAnnotations;

namespace Hæveautomaten.Entities
{
    public class BankEntity
    {
        [Key]
        public uint BankId { get; set; }

        [Required]
        public string BankName { get; set; }

        // Navigation property
        public List<AccountEntity> Accounts { get; set; } = new List<AccountEntity>();

        // Navigation property
        public List<AutomatedTellerMachineEntity> AutomatedTellerMachines { get; set; } = new List<AutomatedTellerMachineEntity>();

        public BankEntity(string bankName)
        {
            BankName = bankName;
        }

        private BankEntity() { }

        public override string ToString()
        {
            return BankName;
        }
    }
}