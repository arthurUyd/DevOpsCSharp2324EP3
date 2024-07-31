using Ardalis.GuardClauses;
using HeyRed.Mime;
using Domain.Common;
namespace Domain.Files;

public class TextFile : ValueObject 
{
    public Uri BasePath { get; }
    public Guid Identifier { get; }
    public string Extension { get; }

    public string Filename => $"{Identifier}.{Extension}";
    public Uri FileUri => new Uri($"{BasePath}/{Filename}");

    public TextFile(Uri basePath, string contentType)
    {
        Identifier = Guid.NewGuid();
        Extension = MimeTypesMap.GetExtension(contentType).ToLower();
        BasePath = Guard.Against.Null(basePath, nameof(basePath));
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Extension.ToLower();
        yield return Identifier;
        yield return BasePath;
    }
}
