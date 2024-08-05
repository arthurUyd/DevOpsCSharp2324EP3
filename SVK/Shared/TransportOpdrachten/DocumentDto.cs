using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.TransportOpdrachten;

public abstract class DocumentDto
{
    public class Index
    {
        public int Id { get; set; }
        public string? Name { get; set; }    
        public string? ContentType { get; set; }
        public byte[]? Content { get; set; }
        public long? Size { get; set; }
    }

    public class Detail
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ContentType { get; set; }
        public byte[]? Content { get; set; }
        public long? Size { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class Mutate
    {
        public IFormFile? File { get; set; }
       

        public class Validator: AbstractValidator<Mutate>
        {
            public Validator() 
            {
                RuleFor(x => x.File).NotNull();
                
            }
        }

    }
}
