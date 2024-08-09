using SVK.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using SVK.Domain.Laadbonnen;

namespace SVK.Domain.Producten;

public class Product : Entity
{
    private string productnaam = default!;
    public string ProductNaam
    {
        get => productnaam;
        set => productnaam = Guard.Against.NullOrWhiteSpace(value, nameof(ProductNaam));
    }

    public List<Laadbon> Laadbonnen { get; set; } = new();
}
