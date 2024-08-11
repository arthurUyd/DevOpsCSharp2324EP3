using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SVK.Shared.Gebruikers;
using Swashbuckle.AspNetCore.Annotations;

namespace SVK.Server.Controllers.Gebruikers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class GebruikerController : ControllerBase
{
    private readonly IGebruikerService gebruikerService;
    
    public GebruikerController(IGebruikerService gebruikerService)
    {
        this.gebruikerService = gebruikerService;
    }


    [SwaggerOperation("Haalt alle gebruikers op.")]
    [HttpGet]
    public async Task<GebruikerResult.Index> GetIndex([FromQuery] GebruikerRequest.Index request)
    {
        return await gebruikerService.GetIndexAsync(request);
    }


}
