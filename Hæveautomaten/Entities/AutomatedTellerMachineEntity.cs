using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Hæveautomaten.Entities
{
    public class AutomatedTellerMachineEntity
    {
        [Key]
        public int AutomatedTellerMachineId { get; set; }

        [Required]
        public uint MinimumExchangeAmount { get; set; }

        // Navigation property
        [ForeignKey("bank")]
        public int BankId { get; set; }

        public BankEntity Bank { get; set; }

        public AutomatedTellerMachineEntity(uint minimumExchangeAmount, BankEntity bank = null)
        {
            MinimumExchangeAmount = minimumExchangeAmount;
            Bank = bank;
        }

        private AutomatedTellerMachineEntity() { }

        public override string ToString()
        {
            return $"ATM ID: {AutomatedTellerMachineId} - Minimum exchange amount: {MinimumExchangeAmount} DKK";
        }
    }
}