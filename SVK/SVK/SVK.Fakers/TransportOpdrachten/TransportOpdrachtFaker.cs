using SVK.Domain.TransportOpdrachten;
using SVK.Fakers.Gebruikers;
using SVK.Fakers.Laadbonnen;
using SVK.Persistence.Fakers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVK.Fakers.TransportOpdrachten;

public class TransportOpdrachtFaker : EntityFaker<TransportOpdracht>
{
    public TransportOpdrachtFaker(string locale = "nl") : base(locale)
    {

        CustomInstantiator(f => new TransportOpdracht(
            f.Date.Past(1),                          // Datum (random past date within 1 year)
            f.Random.Int(1, 1000),                    // Routenummer (random int between 1 and 1000)
            new GebruikerFaker(locale),                // Lader (Gebruiker)
            f.Internet.Url(),                         // FotoUrl (random URL)
            f.Vehicle.Vin(),                          // Nummerplaat (random vehicle VIN)
            new LaadbonFaker(locale).Generate(3)                  // Laadbonnen (list of 3 Laadbonnen)
        ));
    }
}
