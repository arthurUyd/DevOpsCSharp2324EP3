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

    public class Detail
    {
        public int Id { get; set; }
        public string? ProductNaam { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class Mutate
    {
        public string? ProductNaam { get; set; }

        public class Validator : AbstractValidator<Mutate>
        {
            public Validator()
            {
                RuleFor(x => x.ProductNaam).NotEmpty().MaximumLength(100);
                
            }
        }
    }
}
