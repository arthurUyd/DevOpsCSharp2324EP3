using Ardalis.GuardClauses;
using Domain.Common;
using Domain.TransportOpdrachten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Laadbonnen;

public class Laadbon : Entity
{

    private int nummer = default!;
    public int Nummer
    {
        get => nummer;
        set => nummer = Guard.Against.Null(value, nameof(Nummer));
    }

   /* private TransportOpdracht transportOpdracht = default!;

    public TransportOpdracht TransportOpdracht
    {
        get => transportOpdracht;
        set => transportOpdracht = Guard.Against.Null(value,nameof(TransportOpdracht));
    }
*/
    private Laadbon() { }
    public Laadbon(int nummer)
    {
        Nummer = nummer;
    }
}
