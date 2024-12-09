using EcoMonitor.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoMonitor.Core.Repository;

public class EcoContext : DbContext
{
    public EcoContext()
    {
    }

    public EcoContext(DbContextOptions<EcoContext> options)
        : base(options)
    {
    }

    public DbSet<Calculations> Calculations { get; set; }
    public DbSet<Factories> Factories { get; set; }
    public DbSet<Pollutions> Pollutions { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ECO;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Calculations>()
            .HasOne(c => c.Pollution)
            .WithMany(p => p.Calculations)
            .HasForeignKey(c => c.PollutionId)
            .IsRequired();
        modelBuilder.Entity<Calculations>()
            .HasOne(c => c.Factory)
            .WithMany(p => p.Calculations)
            .HasForeignKey(c => c.FactoryId)
            .IsRequired();
    }
}