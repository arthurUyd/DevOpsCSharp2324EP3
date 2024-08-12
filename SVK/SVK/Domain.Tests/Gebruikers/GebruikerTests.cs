using Shouldly;
using SVK.Domain.Gebruikers;
using SVK.Fakers.Gebruikers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tests.Gebruikers;
public class GebruikerTests
{
    [Fact]
    public void Constructor_ShouldInitializeNaam()
    {
        var expectedNaam = "John Doe";

        var gebruiker = new Gebruiker(expectedNaam);

        gebruiker.Naam.ShouldBe(expectedNaam);
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenNaamIsNull()
    {
        var exception = Should.Throw<ArgumentException>(() => new Gebruiker(null!));
        exception.Message.ShouldBe("Value cannot be null. (Parameter 'Naam')");
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenNaamIsEmpty()
    {
        var exception = Should.Throw<ArgumentException>(() => new Gebruiker(string.Empty));
        exception.Message.ShouldBe("Required input Naam was empty. (Parameter 'Naam')");
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenNaamIsWhitespace()
    {
        var exception = Should.Throw<ArgumentException>(() => new Gebruiker("   "));
        exception.Message.ShouldBe("Required input Naam was empty. (Parameter 'Naam')");
    }

    [Fact]
    public void Naam_ShouldThrowArgumentException_WhenSetToNull()
    {
        var gebruiker = new GebruikerFaker().Generate();

        var exception = Should.Throw<ArgumentException>(() => gebruiker.Naam = null!);
        exception.Message.ShouldBe("Value cannot be null. (Parameter 'Naam')");
    }

    [Fact]
    public void Naam_ShouldThrowArgumentException_WhenSetToEmpty()
    {
        var gebruiker = new GebruikerFaker().Generate();

        var exception = Should.Throw<ArgumentException>(() => gebruiker.Naam = string.Empty);
        exception.Message.ShouldBe("Required input Naam was empty. (Parameter 'Naam')");
    }

    [Fact]
    public void Naam_ShouldThrowArgumentException_WhenSetToWhitespace()
    {
        var gebruiker = new GebruikerFaker().Generate();

        var exception = Should.Throw<ArgumentException>(() => gebruiker.Naam = "   ");
        exception.Message.ShouldBe("Required input Naam was empty. (Parameter 'Naam')");
    }

    [Fact]
    public void Naam_ShouldUpdateSuccessfully_WhenSetToValidValue()
    {
        var gebruiker = new GebruikerFaker().Generate();
        var newNaam = "Jane Doe";

        gebruiker.Naam = newNaam;

        gebruiker.Naam.ShouldBe(newNaam);
    }
}