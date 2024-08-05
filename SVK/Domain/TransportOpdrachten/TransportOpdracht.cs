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
using Domain.Laadbonnen;
using Domain.Files;

namespace Domain.TransportOpdrachten;

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
    //laadbon als aparte entity maken? 
    private readonly List<Laadbon> laadbonnen = new();
    public IReadOnlyCollection<Laadbon> Laadbonnen => laadbonnen.AsReadOnly();
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
    //bestanden bekijken met de fotos
    private readonly List<Document> bestanden = new();
    public IReadOnlyCollection<Document> Bestanden => bestanden.AsReadOnly();
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
    public TransportOpdracht(DateTime datum, int routenummer, Gebruiker lader, string foto, string transporteur, string nummerplaat, IEnumerable<Product> producten, IEnumerable<Document> bestandenlijst, IEnumerable<Laadbon> laadbonnenlijst)
    {
        Datum = datum;
        Routenummer = routenummer;
        Guard.Against.NullOrEmpty(laadbonnenlijst, nameof(laadbonnenlijst));
        Guard.Against.Null(bestanden, nameof(bestanden));
        Guard.Against.NullOrEmpty(producten, nameof(producten));
        Lader = lader;
        FotoUrl = foto;
        Transporteur = transporteur;
        Nummerplaat = nummerplaat;

        foreach (Laadbon laadbon in laadbonnenlijst)
        {
            laadbonnen.Add(laadbon);
        }
        foreach (Document s in bestandenlijst)
        {
            bestanden.Add(s);
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

    public void VoegBestandUrlToe(IEnumerable<Document> s)
    {
        Guard.Against.NullOrEmpty(s, nameof(s));
        foreach (Document t in s)
        {
            if (bestanden.Contains(t))
                throw new ApplicationException($"{t} is al toegevoegd aan deze transport opdracht. ");
            bestanden.Add(t);
        }
    }
    
}
