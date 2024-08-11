using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SVK.Shared.Authentication;
using SVK.Shared.Gebruikers;
using SVK.Shared.Laadbonnen;
using Swashbuckle.AspNetCore.Annotations;

namespace SVK.Server.Controllers.Laadbonnen;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LaadbonController : ControllerBase
{

    private readonly ILaadbonService laadbonService;

    public LaadbonController(ILaadbonService laadbonService)
    {
        this.laadbonService = laadbonService;
    }

    [SwaggerOperation("Maakt een laadbon aan.")]
    [HttpPost("{id}")]
    [Authorize(Roles= Roles.Lader)]
    public async Task<IActionResult> Create(int id,LaadbonDto.Mutate model)
    {
        var laadbonId = await laadbonService.CreateAsync(id, model);
        return CreatedAtAction(nameof(Create), laadbonId);
    }
}
