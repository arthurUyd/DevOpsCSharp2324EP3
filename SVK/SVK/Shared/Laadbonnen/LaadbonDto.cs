using FluentValidation;
using SVK.Shared.Producten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVK.Shared.Laadbonnen;

public abstract class LaadbonDto
{
    public class Index
    {
        public int Id { get; set; }
        public int Nummer { get; set; }
        public string? Url { get; set; }

    }
    public class Detail
    {
        public int Id { get; set; }
        public int? Nummer { get; set; }
        public string? Url { get; set; }
        public AddressDto? Address { get; set; }
        public string? Transporteur { get; set; }
        public IEnumerable<ProductDto.Index>? Producten { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
    public class Mutate
    {
        public int? Nummer { get; set; }
        public string? ImageContentType { get; set; }
        public AddressDto Address { get; set; } = new();
        public string? Transporteur { get; set; }
        public List<ProductDto.Index>? Producten { get; set; }

        public class Validator : AbstractValidator<Mutate>
        {
            public Validator()
            {
                RuleFor(x => x.Nummer).NotEmpty().NotNull().GreaterThan(0);
                RuleFor(x => x.ImageContentType).NotEmpty().NotNull();
                RuleFor(x => x.Address).NotEmpty().SetValidator(new AddressDto.Validator());
                RuleFor(x => x.Transporteur).NotEmpty().NotNull();
            }
        }
    }
}
