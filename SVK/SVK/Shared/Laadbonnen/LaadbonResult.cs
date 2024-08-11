using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVK.Shared.Laadbonnen;

public abstract class LaadbonResult
{
 

    public class Create
    {
        public int LaadbonId {  get; set; }
        public string UploadUri { get; set; } = default!;
     
    }
}
