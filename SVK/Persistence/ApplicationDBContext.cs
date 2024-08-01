namespace Persistence;

using Domain.Gebruikers;
using Domain.Producten;
using Domain.TransportOpdracht;
using Microsoft.EntityFrameworkCore;
using Persistence.Triggers;
using System.Reflection;

public class ApplicationDBContext: DbContext
{
    public DbSet<Product> Producten => Set<Product>();
    public DbSet<TransportOpdracht> TransportOpdrachten  => Set<TransportOpdracht>();
    public DbSet<Gebruiker> Gebruikers => Set<Gebruiker>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseInMemoryDatabase(databaseName: "svk");
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

}
