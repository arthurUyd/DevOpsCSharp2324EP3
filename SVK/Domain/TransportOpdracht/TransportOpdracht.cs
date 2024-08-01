using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Gebruikers;
using Domain.Producten;
using Domain.Common;
using Ardalis.GuardClauses;
using System.Collections.ObjectModel;

namespace Domain.TransportOpdracht;

public class TransportOpdracht : Entity
{

    private DateTime datum = default!;
    public DateTime Datum
    {
        get => datum;
        set => datum = Guard.Against.Null(value, nameof(Datum));
    }
    private int routenummer = default!;
    public int Routenummer
    {
        get => routenummer;
        set
        {
            routenummer = Guard.Against.NegativeOrZero(value, nameof(Routenummer));
            routenummer = Guard.Against.Null(value, nameof(Routenummer));
        }
    }

    private readonly List<int> laadbonnummers = new();
    public IReadOnlyCollection<int> Laadbonnummers => laadbonnummers.AsReadOnly();
    public Gebruiker Lader { get; } = default!;
    private string fotourl = default!;
    public string FotoUrl
    {
        get => fotourl;
        set => fotourl = Guard.Against.NullOrWhiteSpace(value, nameof(FotoUrl));
    }
    private readonly List<string> bestandenurls = new();
    public IReadOnlyCollection<string> BestandenUrls => bestandenurls.AsReadOnly();
    private string transporteur = default!;
    public string Transporteur
    {
        get => transporteur;
        set => transporteur = Guard.Against.NullOrWhiteSpace(value, nameof(Transporteur));
    }
    private string nummerplaat = default!;
    public string Nummerplaat
    {
        get => nummerplaat;
        set => nummerplaat = Guard.Against.NullOrWhiteSpace(value, nameof(Nummerplaat));
    }
    private readonly List<Product> products = new();    
    public IReadOnlyCollection<Product> Producten  => products.AsReadOnly();

    private TransportOpdracht() { }
    public TransportOpdracht(DateTime datum, int routenummer, IEnumerable<int> lbn, Gebruiker lader, string foto, IEnumerable<string> bestanden, string transporteur, string nummerplaat, IEnumerable<Product> producten)
    {
        Datum = datum;
        Routenummer = routenummer;
        Guard.Against.NullOrEmpty(lbn, nameof(lbn));
        Guard.Against.NullOrEmpty(bestanden, nameof(bestanden));
        Guard.Against.NullOrEmpty(producten, nameof(producten));
        Lader = Guard.Against.Null(lader, nameof(Lader));
        FotoUrl = foto;
        Transporteur = transporteur;
        Nummerplaat = nummerplaat;

        foreach (int i in lbn)
        {
            laadbonnummers.Add(i);
        }
        foreach(string i in bestanden)
        {
           bestandenurls.Add(i);
        }
        foreach(Product p in producten)
        {
            products.Add(p);
        }
    }

}
