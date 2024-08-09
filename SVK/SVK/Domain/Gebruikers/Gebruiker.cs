using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using SVK.Domain.Common;

namespace SVK.Domain.Gebruikers;

public class Gebruiker : Entity
{
    private string naam = default!;
    public string Naam
    {
        get => naam;
        set => naam = Guard.Against.NullOrWhiteSpace(value, nameof(Naam));
    }
    /*    public Boolean IsLader { get; set; }
    */

    public Gebruiker(string naam)
    {
        Naam = naam;
    }
}
