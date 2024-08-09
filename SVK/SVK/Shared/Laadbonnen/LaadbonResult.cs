using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVK.Shared.Laadbonnen;

public abstract class LaadbonResult
{
    public class Index
    {
        public IEnumerable<LaadbonDto.Index>? Laadbonnen { get; set; }
        public int TotalAmount { get; set; }
    }
}
