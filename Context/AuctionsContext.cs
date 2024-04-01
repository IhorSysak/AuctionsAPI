using AuctionsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionsAPI.Context
{
    public class AuctionsContext : DbContext
    {
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Item> Items { get; set; }

        public AuctionsContext() : base() { }

        public AuctionsContext(DbContextOptions<AuctionsContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Item>()
                .HasOne(i => i.Sale)
                .WithOne(s => s.Item)
                .HasForeignKey<Sale>(s => s.ItemId);

            modelBuilder.Entity<Sale>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,4)");
        }
    }
}
