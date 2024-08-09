using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVK.Domain.Gebruikers;
using SVK.Domain.Producten;
using SVK.Domain.Common;
using Ardalis.GuardClauses;
using System.Collections.ObjectModel;
using SVK.Domain.Laadbonnen;
using SVK.Domain.Files;
using System.Xml.Linq;

namespace SVK.Domain.TransportOpdrachten;

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
   
    private string nummerplaat = default!;
    public string Nummerplaat
    {
        get => nummerplaat;
        set => nummerplaat = Guard.Against.NullOrWhiteSpace(value, nameof(Nummerplaat));
    }
    


    private TransportOpdracht() { }
    public TransportOpdracht(DateTime datum, int routenummer, Gebruiker lader, string foto, string nummerplaat, IEnumerable<Laadbon> laadbonnenlijst)
    {
        Datum = datum;
        Routenummer = routenummer;
        Guard.Against.NullOrEmpty(laadbonnenlijst, nameof(laadbonnenlijst));
        Lader = lader;
        FotoUrl = foto;

        Nummerplaat = nummerplaat;

        foreach (Laadbon laadbon in laadbonnenlijst)
        {
            laadbonnen.Add(laadbon);
        }
       

        
    }

    public TransportOpdracht(DateTime datum1, int routenummer1, Gebruiker lader, string fotourl, string nummerplaat)
    {
        Datum = datum1;
        Routenummer = routenummer1;
        Lader = lader;
        FotoUrl = fotourl;
        Nummerplaat = nummerplaat;
    }

    public void Laadbon(Laadbon laadbon)
    {
        Guard.Against.Null(laadbon, nameof(laadbon));
        if (laadbonnen.Contains(laadbon))
            throw new ApplicationException($"{nameof(TransportOpdracht)} '{Routenummer}' already contains the tag:{laadbon.Nummer}");
        laadbonnen.Add(laadbon);
    }
    
}
