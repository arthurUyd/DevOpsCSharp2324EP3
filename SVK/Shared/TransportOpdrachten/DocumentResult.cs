using Shared.Producten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.TransportOpdrachten;

public abstract class DocumentResult
{
    public class Index
    {
        public IEnumerable<DocumentDto.Index>? Documenten { get; set; }
        public int TotalAmount { get; set; }
    }
}
