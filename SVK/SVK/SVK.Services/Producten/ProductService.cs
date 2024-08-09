using Bogus;
using SVK.Domain.Producten;
using Microsoft.EntityFrameworkCore;
using SVK.Persistence;
using SVK.Shared.Producten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVK.Services.Producten;

public class ProductService : IProductService
{

    private readonly ApplicationDBContext dbContext; 

    public ProductService(ApplicationDBContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<int> CreateAsync(ProductDto.Mutate model)
    {
        if (await dbContext.Producten.AnyAsync(x => x.ProductNaam == model.ProductNaam))
            throw new EntityAlreadyExistsException(nameof(Product), nameof(Product.ProductNaam), model.ProductNaam);

        Product product = new Product()
        {
            ProductNaam = model.ProductNaam!
        };

        dbContext.Producten.Add(product);
        await dbContext.SaveChangesAsync();

        return product.Id;
    }

    public async Task<ProductDto.Detail> GetDetailAsync(int productId)
    {
        ProductDto.Detail? product = await dbContext.Producten.Select(x => new ProductDto.Detail
        {
            Id = x.Id,
            ProductNaam = x.ProductNaam,
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt
        }).SingleOrDefaultAsync(x => x.Id == productId);

        if (product is null)
            throw new EntityNotFoundException(nameof(Product), productId);

        return product;
    }

    public async Task<ProductResult.Index> GetIndexAsync(ProductRequest.Index request)
    {
        var query = dbContext.Producten.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Searchterm))
        {
            query = query.Where(x => x.ProductNaam.Contains(request.Searchterm, StringComparison.OrdinalIgnoreCase));
        }

        int totalAmount = await query.CountAsync();

        var items = await query
           .Skip((request.Page - 1) * request.PageSize)
           .Take(request.PageSize)
           .OrderBy(x => x.Id)
           .Select(x => new ProductDto.Index
           {
               Id = x.Id,
               ProductNaam = x.ProductNaam,
           }).ToListAsync();

        var result = new ProductResult.Index
        {
            Products = items,
            TotalAmount = totalAmount
        };
        return result;
    }
}
