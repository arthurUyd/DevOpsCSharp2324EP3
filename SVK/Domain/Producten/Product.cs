using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;

namespace Domain.Producten
{
    public class Product : Entity
    {
        private string productnaam = default!;
        public string ProductNaam
        {
            get => productnaam;
            set => productnaam = Guard.Against.NullOrWhiteSpace(value, nameof(ProductNaam));
        }

        private Product() { }
        public Product(string productNaam)
        {
            ProductNaam = productNaam;
        }
    }
}
