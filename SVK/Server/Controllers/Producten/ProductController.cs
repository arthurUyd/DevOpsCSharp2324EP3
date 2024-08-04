using Microsoft.AspNetCore.Mvc;
using Shared.Producten;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.Producten;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService productService;

    public ProductController(IProductService productService)
    {
        this.productService = productService;
    }

    [SwaggerOperation("Haalt alle producten op.")]
    [HttpGet]
    public async Task<ProductResult.Index> GetIndex([FromQuery] ProductRequest.Index request)
    {
        return await productService.GetIndexAsync(request);
    }

    [SwaggerOperation("Haalt een product op aan de hand van zijn Id.")]
    [HttpGet("{id}")]
    public async Task<ProductDto.Detail> GetDetail(int id)
    {
        return await productService.GetDetailAsync(id);
    }

    [SwaggerOperation("Maakt een nieuw product aan.")]
    [HttpPost]
    public async Task<IActionResult> Create(ProductDto.Mutate model)
    {
        var productId = await productService.CreateAsync(model);  
        return CreatedAtAction(nameof(Create), productId);
    }
}
