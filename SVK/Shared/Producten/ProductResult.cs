using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Producten;

public abstract class ProductResult
{
    public class Index
    {
        public IEnumerable<ProductDto.Index>? Products { get; set; }
        public int TotalAmount { get; set; }
    }
}
