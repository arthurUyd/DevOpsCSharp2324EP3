using Domain.Files;
using Domain.Gebruikers;
using Domain.Laadbonnen;
using Domain.TransportOpdrachten;

using NHibernate.Util;
using Persistence.Faker;
using Persistence.Helpers;
using System.ComponentModel.DataAnnotations;

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
        dbContext.Gebruikers.AddRange(gebruikers);
        dbContext.SaveChanges();
    }

    private void SeedTransportOpdrachten()
    {
        var lbn = new List<Laadbon> { new(1400154381), new(1400154382), new(1400154380) };
        Document Document1 = MakeDocumentFromFilepath("D:\\Users\\arthu\\Documents\\school\\Y3\\DevOps\\DevOpsCSharp2324EP3\\SVK\\Persistence\\Helpers\\Files\\007_laadbon_1400154382_R39492.pdf");
        Document Document2 = MakeDocumentFromFilepath("D:\\Users\\arthu\\Documents\\school\\Y3\\DevOps\\DevOpsCSharp2324EP3\\SVK\\Persistence\\Helpers\\Files\\008_laadbon_1400154381_R39492.pdf");
        Document Document3 = MakeDocumentFromFilepath("D:\\Users\\arthu\\Documents\\school\\Y3\\DevOps\\DevOpsCSharp2324EP3\\SVK\\Persistence\\Helpers\\Files\\009_laadbon_1400154380_R39492.pdf");

        var documenten = new List<Document> { Document1, Document2, Document3 };

        TransportOpdracht transportOpdracht = new TransportOpdracht(DateTime.Now, 39492, new Gebruiker("Kenny"), "https://hogentsvk.blob.core.windows.net/images/015_svk_logo_met_slogan_black-01.jpg", "VANOVERSCHELDE FOURAGES BVBA HUIFWAGEN ZONDER KOOIAAP", "1-VGD-518 / Q-ALJ-972", new ProductFaker().AsTransient().UseSeed(1233).Generate(12),documenten, lbn);

        dbContext.TransportOpdrachten.Add(transportOpdracht);
        dbContext.SaveChanges();
    }

    private Document MakeDocumentFromFilepath(string filepath)
    {
        byte[] content = FileHelper.FileToByteArray(filepath);
        var document = new Document
        (
            Path.GetFileName(filepath),
            "application/pdf", // Or determine dynamically
            content.Length,
            content
        );
        return document;
    }
}
