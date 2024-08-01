using FluentValidation;
using Shared.Producten;
using Shared.Gebruikers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.TransportOpdrachten;

public abstract class TransportOpdrachtDto
{
    public class Index
    {
        public int Id { get; set; }
        public DateTime? Datum { get; set; }
        public int? Routenummer { get; set; }
        public IEnumerable<int>? Laadbonnummers { get; set; }
        public GebruikerDto.Index? Lader { get; set; }
        public string? Fotourl { get; set; }
        public IEnumerable<string>? BestandenUrls { get; set; }
        public string? Transporteur { get; set; }
        public string? Nummerplaat { get; set; }
        public IEnumerable<string>? Producten {  get; set; }
    }
    
    public class Detail
    {
        public int Id { get; set; }
        public DateTime? Datum { get; set; }
        public int? Routenummer { get; set; }
        public IEnumerable<int>? Laadbonnummers { get; set; }
        public GebruikerDto.Index? Lader { get; set; }
        public string? Fotourl { get; set; }
        public IEnumerable<string>? BestandenUrls { get; set; }
        public string? Transporteur { get; set; }
        public string? Nummerplaat { get; set; }
        public IEnumerable<string>? Producten { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
    }

    public class Mutate
    {
        public DateTime? Datum { get; set; }
        public int? Routenummer { get; set; }
        public string? Lader { get; set; }
        public string? ImageContentType { get; set; }
        public string? Transporteur { get; set; }
        public string? Nummerplaat { get; set; }
       
        public class Validator  : AbstractValidator<Mutate>
        {
            public Validator() { 
                RuleFor(x => x.Routenummer).NotEmpty();
                RuleFor(x => x.Lader).NotEmpty();
                RuleFor(x => x.ImageContentType).NotEmpty();
                RuleFor(x => x.Transporteur).NotEmpty();
                RuleFor(x => x.Nummerplaat).NotEmpty();
            }
        }
    }
}
