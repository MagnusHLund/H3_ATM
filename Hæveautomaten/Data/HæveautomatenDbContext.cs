using Hæveautomaten.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hæveautomaten.Data
{
    public class HæveautomatenDbContext : DbContext
    {
        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<PersonEntity> Persons { get; set; }
        public DbSet<CreditCardEntity> CreditCards { get; set; }
        public DbSet<BankEntity> Banks { get; set; }
        public DbSet<AutomatedTellerMachineEntity> AutomatedTellerMachines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // AccountEntity Configuration
            modelBuilder.Entity<AccountEntity>()
                .HasKey(a => a.AccountId);

            modelBuilder.Entity<AccountEntity>()
                .HasOne(a => a.Bank)
                .WithMany(b => b.Accounts)
                .HasForeignKey("BankId")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AccountEntity>()
                .HasMany(a => a.CreditCards)
                .WithOne(c => c.Account)
                .HasForeignKey("AccountNumber")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AccountEntity>()
                .HasOne(a => a.AccountOwner)
                .WithMany(p => p.Accounts)
                .HasForeignKey("PersonId")
                .OnDelete(DeleteBehavior.Restrict);

            // CreditCardEntity Configuration
            modelBuilder.Entity<CreditCardEntity>()
                .HasKey(c => c.CardNumber);

            modelBuilder.Entity<CreditCardEntity>()
                .HasOne(c => c.Account)
                .WithMany(a => a.CreditCards)
                .HasForeignKey("AccountNumber")
                .OnDelete(DeleteBehavior.Restrict);

            // PersonEntity Configuration
            modelBuilder.Entity<PersonEntity>()
                .HasKey(p => p.PersonId);

            modelBuilder.Entity<PersonEntity>()
                .HasMany(p => p.Accounts)
                .WithOne()
                .HasForeignKey("CardHolderName")
                .OnDelete(DeleteBehavior.Restrict);

            // BankEntity Configuration
            modelBuilder.Entity<BankEntity>()
                .HasKey(b => b.BankId);

            modelBuilder.Entity<BankEntity>()
                .HasMany(b => b.Accounts)
                .WithOne(a => a.Bank)
                .HasForeignKey("BankId")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BankEntity>()
                .HasMany(b => b.AutomatedTellerMachines)
                .WithOne()
                .HasForeignKey("BankId")
                .OnDelete(DeleteBehavior.Cascade);

            // AutomatedTellerMachineEntity Configuration
            modelBuilder.Entity<AutomatedTellerMachineEntity>()
                .HasKey(atm => atm.AutomatedTellerMachineId);

            modelBuilder.Entity<AutomatedTellerMachineEntity>()
                .HasOne<BankEntity>()
                .WithMany(b => b.AutomatedTellerMachines)
                .HasForeignKey("BankId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}