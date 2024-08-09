using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVK.Shared.TransportOpdrachten;

public abstract class TransportOpdrachtResult
{
    public class Index
    {
        public IEnumerable<TransportOpdrachtDto.Index>? TransportOpdrachten { get; set; }
        
        public int TotalAmount { get; set; }
    }
    public class Create
    {
        public int TransportOpdrachtId { get; set; }
        public string UploadUri { get; set; } = default!;
    }
}

