
using Microsoft.EntityFrameworkCore;
using SVK.Domain.Gebruikers;
using SVK.Domain.Laadbonnen;
using SVK.Domain.Producten;
using SVK.Domain.TransportOpdrachten;
using SVK.Persistence.Triggers;
using System.Reflection;
using System.Reflection.Metadata;
namespace SVK.Persistence;

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
        modelBuilder.Entity<Laadbon>()
            .HasMany(e => e.Producten)
            .WithMany(e => e.Laadbonnen)
            .UsingEntity("ProductLaadbon",
            l => l.HasOne(typeof(Product)).WithMany().HasForeignKey("ProductId").HasPrincipalKey(nameof(Product.Id)),
            r => r.HasOne(typeof(Laadbon)).WithMany().HasForeignKey("LaadbonId").HasPrincipalKey(nameof(Laadbon.Id)),
            j => j.HasKey("LaadbonId", "ProductId"));
        // In your DbContext or via Fluent API
        
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
