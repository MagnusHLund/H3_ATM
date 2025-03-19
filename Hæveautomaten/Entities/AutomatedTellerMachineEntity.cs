using System.ComponentModel.DataAnnotations;

namespace Hæveautomaten.Entities
{
    public class AutomatedTellerMachineEntity
    {
        [Key]
        public uint AutomatedTellerMachineId { get; set; }

        [Required]
        public uint MinimumExchangeAmount { get; set; }

        // Navigation property
        public BankEntity Bank { get; set; }

        public AutomatedTellerMachineEntity(uint minimumExchangeAmount)
        {
            MinimumExchangeAmount = minimumExchangeAmount;
        }
    }
}