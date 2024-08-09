using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVK.Shared.Gebruikers;

public abstract class GebruikerResult
{
    public class Index
    {
        public IEnumerable<GebruikerDto.Index>? Gebruikers { get; set; }
        public int TotalAmount { get; set; }
    }

   
}
