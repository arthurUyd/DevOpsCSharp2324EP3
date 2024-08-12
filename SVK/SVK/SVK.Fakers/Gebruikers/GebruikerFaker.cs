using SVK.Domain.Gebruikers;
using SVK.Domain.Producten;
using SVK.Persistence.Fakers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVK.Fakers.Gebruikers;

public class GebruikerFaker: EntityFaker<Gebruiker>
{
    public GebruikerFaker(string locale = "nl") : base(locale)
    {
        CustomInstantiator(f => new Gebruiker(f.Name.FullName()));
    }
}
