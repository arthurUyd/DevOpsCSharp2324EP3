using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Shared.Gebruikers;

public class GebruikerDto
{
    public class Index
    {
        public Guid Id { get; set; }
        public string Naam { get; set; }
    }

    public class Mutate
    {
        public string? Naam { get; set; }

        public class Validator: AbstractValidator<Mutate>
        {
            public Validator() {
                RuleFor(x => x.Naam).NotEmpty().WithMessage("Naam mag niet leeg zijn.").WithName("Naam");
            }
        }
    }
}
