using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Laadbonnen;

public abstract class LaadbonDto
{
    public class Index
    {
        public int Id { get; set; }
        public int Nummer { get; set; }
    }
    public class Detail
    {
        public int Id { get; set; }
        public int? Nummer { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
    public class Mutate
    {
        public int? Nummer { get; set; }

        public class Validator : AbstractValidator<Mutate>
        {
            public Validator()
            {
                RuleFor(x => x.Nummer).NotEmpty().NotNull().GreaterThan(0);

            }
        }
    }
}
