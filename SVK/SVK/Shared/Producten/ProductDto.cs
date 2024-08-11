using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVK.Shared.Producten;

public abstract class ProductDto
{
    public class Index
    {
        public int Id { get; set; }
        public string? ProductNaam { get; set; }
    }

  
}
