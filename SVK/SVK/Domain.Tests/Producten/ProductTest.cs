using SVK.Domain.Laadbonnen;
using SVK.Domain.Producten;
using SVK.Fakers.Laadbonnen;
using SVK.Persistence.Faker;
using Shouldly;
using System;
using Xunit;

namespace Domain.Tests.Producten
{
    public class ProductTest
    {
        [Fact]
        public void Constructor_ShouldInitializeProductName()
        {
            var productName = "Sample Product";

            var product = new Product(productName);

            product.ProductNaam.ShouldBe(productName);
        }

        [Fact]
        public void Constructor_ShouldInitializeEmptyLaadbonnenList()
        {
            var product = new ProductFaker().Generate();

            product.Laadbonnen.ShouldNotBeNull();
            product.Laadbonnen.ShouldBeEmpty();
        }

        [Fact]
        public void ProductNaam_Set_ThrowsArgumentNullException_WhenNull()
        {
            var product = new ProductFaker().Generate();

            var exception = Should.Throw<ArgumentNullException>(() => product.ProductNaam = null);
            exception.ParamName.ShouldBe("ProductNaam");
        }

        [Fact]
        public void ProductNaam_Set_ThrowsArgumentException_WhenEmpty()
        {
            var product = new ProductFaker().Generate();

            var exception = Should.Throw<ArgumentException>(() => product.ProductNaam = string.Empty);
            exception.ParamName.ShouldBe("ProductNaam");
        }

        [Fact]
        public void ProductNaam_Set_ThrowsArgumentException_WhenWhitespace()
        {
            var product = new ProductFaker().Generate();

            var exception = Should.Throw<ArgumentException>(() => product.ProductNaam = "   ");
            exception.ParamName.ShouldBe("ProductNaam");
        }
    }
}
