using SVK.Domain.Files;
using SVK.Domain.Gebruikers;
using SVK.Domain.Laadbonnen;
using SVK.Domain.TransportOpdrachten;

using SVK.Persistence.Faker;
using System.ComponentModel.DataAnnotations;


namespace SVK.Persistence;

public class Seeder
{
    private readonly ApplicationDBContext dbContext;

    public Seeder(ApplicationDBContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Seed()
    {
#if DEBUG
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
#endif
        SeedProducts();
        SeedGebruikers();
        SeedTransportOpdrachten();
    }

    private void SeedProducts()
    {

        var products = new ProductFaker().AsTransient().UseSeed(3215).Generate(50);
        dbContext.Producten.AddRange(products);
        dbContext.SaveChanges();
    }

    private void SeedGebruikers()
    {
        List<Gebruiker> gebruikers = new()
        {
            new Gebruiker("Thomas" ),
            new Gebruiker("Hendrik" ),
            new Gebruiker("Lucas" ),
            new Gebruiker("Emma" ),
            new Gebruiker("Sarah" ),
            new Gebruiker("Ward" )
        };
        dbContext.Gebruikers.AddRange(gebruikers);
        dbContext.SaveChanges();
    }

    private void SeedTransportOpdrachten()
    {
        // Create Address instances (no need to add them to the context separately)
        Address a = new Address("straatnaam 1", "straatnaam1", "9890", "Gavere", "Belgium");
        Address b = new Address("straatnaam 2", "straatnaam2", "9000", "Gent", "Belgium");
        Address c = new Address("straatnaam 3", "straatnaam3", "2300", "Brussel", "Belgium");

        // Create Laadbon instances with Address as part of the constructor
        Laadbon l1 = new Laadbon(1400154382, "https://hogentsvk.blob.core.windows.net/images/007_laadbon_1400154382_R39492.pdf", a, new ProductFaker().AsTransient().UseSeed(1233).Generate(3), "VANOVERSCHELDE FOURAGES BVBA HUIFWAGEN ZONDER KOOIAAP");
        Laadbon l2 = new Laadbon(1400154381, "https://hogentsvk.blob.core.windows.net/images/008_laadbon_1400154381_R39492.pdf", b, new ProductFaker().AsTransient().UseSeed(1234).Generate(3), "VANOVERSCHELDE FOURAGES BVBA HUIFWAGEN ZONDER KOOIAAP");
        Laadbon l3 = new Laadbon(1400154380, "https://hogentsvk.blob.core.windows.net/images/009_laadbon_1400154380_R39492.pdf", c, new ProductFaker().AsTransient().UseSeed(1235).Generate(3), "VANOVERSCHELDE FOURAGES BVBA HUIFWAGEN ZONDER KOOIAAP");

        // Add Laadbon instances to the context
        List<Laadbon> lbn = new(new[] { l1, l2, l3 });
        dbContext.Laadbonnen.AddRange(lbn);

        // Create and add TransportOpdracht
        TransportOpdracht transportOpdracht = new TransportOpdracht(DateTime.Now, 39492, new Gebruiker("Kenny"), "https://hogentsvk.blob.core.windows.net/images/015_svk_logo_met_slogan_black-01.jpg", "1-VGD-518 / Q-ALJ-972", lbn);
        dbContext.TransportOpdrachten.Add(transportOpdracht);

        // Save changes to the database
        dbContext.SaveChanges();
    }
}
