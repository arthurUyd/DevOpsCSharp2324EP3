using SVK.Domain.Laadbonnen;
using SVK.Fakers.Common;
using SVK.Persistence.Faker;
using SVK.Persistence.Fakers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVK.Fakers.Laadbonnen;

public class LaadbonFaker: EntityFaker<Laadbon>
{
    public LaadbonFaker(string locale = "nl"): base(locale)
    {
        CustomInstantiator(f => new Laadbon(
            f.Random.Int(1, 10000),                       // Nummer
            f.Internet.Url(),                             // Bestandurl
            new AddressFaker(locale),                      // Address
            new ProductFaker(locale).Generate(3),                     // Producten (list of 3 products)
            f.Company.CompanyName()                       // Transporteur
        ));
    }
}
