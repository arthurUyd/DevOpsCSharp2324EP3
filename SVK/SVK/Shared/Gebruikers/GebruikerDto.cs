using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SVK.Shared.Gebruikers;

public abstract class GebruikerDto
{
    public class Index
    {
        public int Id { get; set; }
        public string? Naam { get; set; }
    }

    public class Detail
    {
        public int Id { get; set; }
        public string? Naam { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
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
