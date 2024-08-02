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
    private Gebruiker lader = default!;
    public Gebruiker Lader
    {
        get => lader;
        set => lader = Guard.Against.Null(value, nameof(Lader));
    }
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
    public IReadOnlyCollection<Product> Producten => products.AsReadOnly();



    private TransportOpdracht() { }
    public TransportOpdracht(DateTime datum, int routenummer, IEnumerable<int> lbn, Gebruiker lader, string foto, IEnumerable<string> bestanden, string transporteur, string nummerplaat, IEnumerable<Product> producten)
    {
        Datum = datum;
        Routenummer = routenummer;
        Guard.Against.NullOrEmpty(lbn, nameof(lbn));
        Guard.Against.NullOrEmpty(bestanden, nameof(bestanden));
        Guard.Against.NullOrEmpty(producten, nameof(producten));
        Lader = lader;
        FotoUrl = foto;
        Transporteur = transporteur;
        Nummerplaat = nummerplaat;

        foreach (int i in lbn)
        {
            laadbonnummers.Add(i);
        }
        foreach (string i in bestanden)
        {
            bestandenurls.Add(i);
        }
        foreach (Product p in producten)
        {
            products.Add(p);
        }
    }

    public TransportOpdracht(DateTime datum1, int routenummer1, Gebruiker lader, string fotourl, string transporteur, string nummerplaat)
    {
        Datum = datum1;
        Routenummer = routenummer1;
        Lader = lader;
        FotoUrl = fotourl;
        Transporteur = transporteur;
        Nummerplaat = nummerplaat;
    }

    public void Product(Product p)
    {
        Guard.Against.Null(p, nameof(p));
        products.Add(p);
    }

    public void VoegBestandUrlToe(string s)
    {
        Guard.Against.NullOrWhiteSpace(s, nameof(s));
        if (bestandenurls.Contains(s))
            throw new ApplicationException($"{s} is al toegevoegd aan deze transport opdracht. ");
        bestandenurls.Add(s);

    }
    public void VoegFotoUrlToe(int s)
    {
        Guard.Against.Null(s, nameof(s));
        if (laadbonnummers.Contains(s))
            throw new ApplicationException($"{s} is al toegevoegd aan deze transport opdracht. ");
        laadbonnummers.Add(s);
    }
}
