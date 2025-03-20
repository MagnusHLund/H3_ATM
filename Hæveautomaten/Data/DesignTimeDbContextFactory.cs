using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Hæveautomaten.Data;

namespace Hæveautomaten
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<HæveautomatenDbContext>
    {
        public HæveautomatenDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HæveautomatenDbContext>();
            optionsBuilder.UseSqlite("Data Source=Data\\atm.db");

            return new HæveautomatenDbContext(optionsBuilder.Options);
        }
    }
}
