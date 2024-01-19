using CashFlowTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlowTracker.Infra.Data
{
    public class SqlServerContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost,1433;Database=CashFlowTrackerDb;User Id=sa;Password=OurStrong!Passw0rd;Encrypt=False;TrustServerCertificate=True");
            }
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<DailyBalance> DailyBalances { get; set; }
        public DbSet<ConsolidationLog> ConsolidationLogs { get; set; }
    }
}
