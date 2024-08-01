using Domain.Gebruikers;
using Domain.TransportOpdracht;
using Persistence.Faker;


namespace Persistence;

public class Seeder
{
    private readonly ApplicationDBContext dbContext;

    public Seeder(ApplicationDBContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Seed()
    {
        SeedProducts();
        SeedGebruikers();
        SeedTransportOpdrachten();
    }

    private void SeedProducts()
    {
        var products = new ProductFaker().AsTransient().UseSeed(1233).Generate(50);
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
        dbContext.Gebruikers.AddRange( gebruikers );
        dbContext.SaveChanges();
    }

    private void SeedTransportOpdrachten()
    {
        TransportOpdracht transportOpdracht = new TransportOpdracht(DateTime.Now,39492, new List<int>{1400154381, 1400154382, 1400154380}, new Gebruiker("Kenny"),"https://hogentsvk.blob.core.windows.net/images/015_svk_logo_met_slogan_black-01.jpg", new List<string> { "https://hogentsvk.blob.core.windows.net/images/006_transportopdracht_route_39492.pdf", "https://hogentsvk.blob.core.windows.net/images/007_laadbon_1400154382_R39492.pdf", "https://hogentsvk.blob.core.windows.net/images/008_laadbon_1400154381_R39492.pdf", "https://hogentsvk.blob.core.windows.net/images/009_laadbon_1400154380_R39492.pdf" }, "VANOVERSCHELDE FOURAGES BVBA HUIFWAGEN ZONDER KOOIAAP", "1-VGD-518 / Q-ALJ-972",  new ProductFaker().AsTransient().UseSeed(1233).Generate(12));
        dbContext.TransportOpdrachten.Add(transportOpdracht);
        dbContext.SaveChanges();
    }
}
