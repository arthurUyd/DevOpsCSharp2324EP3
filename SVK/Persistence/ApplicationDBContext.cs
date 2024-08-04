namespace Persistence;

using Domain.Gebruikers;
using Domain.Laadbonnen;
using Domain.Producten;
using Domain.TransportOpdrachten;
using Microsoft.EntityFrameworkCore;
using Persistence.Triggers;
using System.Reflection;

public class ApplicationDBContext: DbContext
{
    public DbSet<Product> Producten => Set<Product>();
    public DbSet<TransportOpdracht> TransportOpdrachten  => Set<TransportOpdracht>();
    public DbSet<Gebruiker> Gebruikers => Set<Gebruiker>();
    public DbSet<Laadbon> Laadbonnen => Set<Laadbon>();
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseTriggers(options =>
        {
            options.AddTrigger<EntityBeforeSaveTrigger>();
        });
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        // All decimals should have 2 digits after the comma
        configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        // Max Length of a NVARCHAR that can be indexed
        configurationBuilder.Properties<string>().HaveMaxLength(4_000);
    }

}
