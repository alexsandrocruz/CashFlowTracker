using CashFlowTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlowTracker.Infra.Data
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountId);

            modelBuilder.Entity<DailyBalance>()
                .HasOne(d => d.Account)
                .WithMany(a => a.DailyBalances)
                .HasForeignKey(d => d.AccountId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Category)
                .WithMany()
                .HasForeignKey(t => t.CategoryId)
                .IsRequired(false); 
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<DailyBalance> DailyBalances { get; set; }
        public DbSet<ConsolidationLog> ConsolidationLogs { get; set; }
    }
}
