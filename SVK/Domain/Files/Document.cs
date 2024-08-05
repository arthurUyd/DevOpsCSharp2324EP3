using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Ardalis.GuardClauses;
namespace Domain.Files;

public class Document: Entity
{
    
    public string Name { get; set; } = default!;

    public string ContentType { get; set; } = default!;

    public long Size { get; set; } = default!;

    public byte[] Content { get; set; } = default!;

    private Document() { }

    public Document(string name, string contentType, long size, byte[] content)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(Name));
        ContentType = Guard.Against.NullOrWhiteSpace(contentType, nameof(ContentType));
        Size = Guard.Against.Null(size, nameof(Size));
        Content = Guard.Against.Null(content, nameof(Content));
    }
    public Document(string name, string contentType, long size)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(Name));
        ContentType = Guard.Against.NullOrWhiteSpace(contentType, nameof(ContentType));
        Size = Guard.Against.Null(size, nameof(Size));
    }
}
