using Ardalis.GuardClauses;
using SVK.Domain.Common;
using SVK.Domain.Producten;
using SVK.Domain.TransportOpdrachten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVK.Domain.Laadbonnen;

public class Laadbon : Entity
{

    private int nummer = default!;
    public int Nummer
    {
        get => nummer;
        set => nummer = Guard.Against.Null(value, nameof(Nummer));
    }
    private string bestandurl = default!;
    public string Bestandurl
    {
        get => bestandurl;
        set => bestandurl = Guard.Against.NullOrWhiteSpace(value, nameof(Bestandurl));
    }
    private Address address = default!;
    public Address Address
    {
        get => address;
        set 
        {
            if (value == null)
                throw new ArgumentNullException(nameof(Address), "Address cannot be null.");
            address = value;
        }
    }
    private string transporteur = default!;
    public string Transporteur
    {
        get => transporteur;
        set => transporteur = Guard.Against.NullOrWhiteSpace(value, nameof(Transporteur));
    }

    private readonly List<Product> producten = new();
    public IReadOnlyCollection<Product> Producten => producten.AsReadOnly();

    private Laadbon() { }
    public Laadbon(int nummer, string bestandurl, Address adres, IEnumerable<Product> producten, string transporteur)
    {
        Nummer = nummer;
        Bestandurl = bestandurl;
        Address = adres;
        Transporteur = transporteur;
        Guard.Against.NullOrEmpty(producten, nameof(Producten));

        this.producten.AddRange(producten);
    }
    public Laadbon(int nummer, string bestandurl, Address adres, string transporteur)
    {
        Nummer = nummer;
        Bestandurl = bestandurl;
        Address = adres;
        Transporteur = transporteur;
    }
}
