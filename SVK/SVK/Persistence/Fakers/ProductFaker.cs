
using Bogus;
using SVK.Persistence.Fakers;
using SVK.Domain.Producten;
namespace SVK.Persistence.Faker;

public class ProductFaker : EntityFaker<Product>
{
    public ProductFaker(string locale = "nl") : base(locale)
    {
        CustomInstantiator(f => new Product()
        {
            ProductNaam =f.Commerce.ProductName()
        });
    }
}