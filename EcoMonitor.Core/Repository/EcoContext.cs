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
    public DbSet<RadioCreation> Creations { get; set; }
    public DbSet<TempStorage> Storages { get; set; }

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
        modelBuilder.Entity<RadioCreation>()
            .HasOne(c => c.Factory)
            .WithMany(p => p.Creations)
            .HasForeignKey(c => c.FactoryId)
            .IsRequired();
        modelBuilder.Entity<TempStorage>()
            .HasOne(c => c.Factory)
            .WithMany(p => p.Storages)
            .HasForeignKey(c => c.FactoryId)
            .IsRequired();
        modelBuilder.Entity<Risk>()
            .HasOne(r => r.Factory)
            .WithMany(f => f.Risks)
            .HasForeignKey(r => r.FactoryId)
            .IsRequired();
        modelBuilder.Entity<Risk>()
            .HasOne(r => r.Pollution)
            .WithMany(f => f.Risks)
            .HasForeignKey(r => r.PollutionId)
            .IsRequired();
    }
}