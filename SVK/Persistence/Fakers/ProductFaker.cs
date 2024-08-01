
using Bogus;
using Persistence.Fakers;
using Domain.Producten;
namespace Persistence.Faker;

public class ProductFaker : EntityFaker<Product>
{
    public ProductFaker(string locale = "nl") : base(locale)
    {
        CustomInstantiator(f => new Product(f.Commerce.ProductName()));
    }
}