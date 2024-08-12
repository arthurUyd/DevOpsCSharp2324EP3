using Bogus;
using Shouldly;
using SVK.Domain.Gebruikers;
using SVK.Domain.Laadbonnen;
using SVK.Domain.TransportOpdrachten;
using SVK.Fakers.Gebruikers;
using SVK.Fakers.Laadbonnen;
using SVK.Fakers.TransportOpdrachten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tests.TransportOpdrachten;

public class TransportOpdrachtTests
{
    private readonly GebruikerFaker GebruikerFaker = new();
    private readonly LaadbonFaker LaadbonFaker = new();

   

    [Fact]
    public void Constructor_ShouldInitializeProperties_WithValidInputs()
    {
        var datum = DateTime.Now;
        var routenummer = 123;
        var lader = GebruikerFaker.Generate();
        var fotourl = "http://example.com/photo.jpg";
        var nummerplaat = "ABC-123";
        var laadbonnen = LaadbonFaker.Generate(2);

        var transportOpdracht = new TransportOpdracht(datum, routenummer, lader, fotourl, nummerplaat, laadbonnen);

        transportOpdracht.Datum.ShouldBe(datum);
        transportOpdracht.Routenummer.ShouldBe(routenummer);
        transportOpdracht.Lader.ShouldBe(lader);
        transportOpdracht.FotoUrl.ShouldBe(fotourl);
        transportOpdracht.Nummerplaat.ShouldBe(nummerplaat);
        transportOpdracht.Laadbonnen.Count.ShouldBe(2);
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenRoutenummerIsZeroOrNegative()
    {
        TransportOpdracht t = new TransportOpdrachtFaker().Generate();

        var exception = Should.Throw<ArgumentException>(() => t.Routenummer = -1);
        exception.ParamName.ShouldBe("Routenummer");
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenLaderIsNull()
    {
        TransportOpdracht t = new TransportOpdrachtFaker().Generate();


        var exception = Should.Throw<ArgumentNullException>(() => t.Lader = null);
        exception.ParamName.ShouldBe("Lader");
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenFotoUrlIsNullOrWhitespace()
    {
     
        TransportOpdracht t = new TransportOpdrachtFaker().Generate();

        var exception = Should.Throw<ArgumentException>(() => t.FotoUrl = null);
        exception.ParamName.ShouldBe("FotoUrl");
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenNummerplaatIsNullOrWhitespace()
    {
        TransportOpdracht t = new TransportOpdrachtFaker().Generate();

        var exception = Should.Throw<ArgumentException>(() => t.Nummerplaat = null);
        exception.ParamName.ShouldBe("Nummerplaat");
    }

    [Fact]
    public void Laadbon_ShouldThrowApplicationException_WhenLaadbonAlreadyExists()
    {
        var datum = DateTime.Now;
        var routenummer = 123;
        var lader = GebruikerFaker.Generate();
        var fotourl = "http://example.com/photo.jpg";
        var nummerplaat = "ABC-123";
        var laadbon = LaadbonFaker.Generate();
        var transportOpdracht = new TransportOpdracht(datum, routenummer, lader, fotourl, nummerplaat, new List<Laadbon> { laadbon });

        var exception = Should.Throw<ApplicationException>(() => transportOpdracht.Laadbon(laadbon));
        exception.Message.ShouldContain($"'{routenummer}' already contains the tag:{laadbon.Nummer}");
    }
}
