using Microsoft.AspNetCore.Mvc;
using SVK.Shared.Producten;
using Swashbuckle.AspNetCore.Annotations;

namespace SVK.Server.Controllers.Producten;

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

}
