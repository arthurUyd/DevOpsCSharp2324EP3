using System;
using System.Collections.Generic;
using Shouldly;
using SVK.Domain.Laadbonnen;
using SVK.Domain.Producten;

using Bogus;
using SVK.Fakers.Common;
using SVK.Persistence.Faker;
using SVK.Fakers.Laadbonnen;

namespace Domain.Tests.Laadbonnen;



public class LaadbonTests
{
    private readonly AddressFaker AddressFaker = new();
    private readonly ProductFaker ProductFaker = new();

    [Fact]
    public void Constructor_ShouldInitializeProperties_WithValidInputs()
    {
        var nummer = 123;
        var bestandurl = "http://example.com/file.pdf";
        var address = AddressFaker.Generate();
        var transporteur = "Transporteur A";
        var producten = ProductFaker.Generate(2);

        var laadbon = new Laadbon(nummer, bestandurl, address, producten, transporteur);

        laadbon.Nummer.ShouldBe(nummer);
        laadbon.Bestandurl.ShouldBe(bestandurl);
        laadbon.Address.ShouldBe(address);
        laadbon.Transporteur.ShouldBe(transporteur);
        laadbon.Producten.Count.ShouldBe(2);
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenNummerIsNull()
    {
        Laadbon l = new LaadbonFaker().Generate(); 
        var exception = Should.Throw<ArgumentException>(() => l.Nummer = -1);
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenAddressIsNull()
    {
        Laadbon l = new LaadbonFaker().Generate();

        var exception = Should.Throw<ArgumentNullException>(() => l.Address = null);
        exception.ParamName.ShouldBe("Address");
        exception.Message.ShouldContain("Address cannot be null.");
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenProductenIsNullOrEmpty()
    {
        var nummer = 123;
        var bestandurl = "http://example.com/file.pdf";
        var address = AddressFaker.Generate();
        var transporteur = "Transporteur A";

        var exception = Should.Throw<ArgumentException>(() => new Laadbon(nummer, bestandurl, address, new List<Product>(), transporteur));
        exception.ParamName.ShouldBe("Producten");
    }
}
