using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SVK.Shared.TransportOpdrachten;
using Swashbuckle.AspNetCore.Annotations;

namespace SVK.Server.Controllers.TransportOpdracht;

[ApiController]
[Route("api/[controller]")]
public class TransportOpdrachtController : ControllerBase
{
    private readonly ITransportOpdrachtService service; 

    public TransportOpdrachtController(ITransportOpdrachtService service)
    {
        this.service = service;
    }

    [SwaggerOperation("Haalt alle transportOpdrachten op.")]
    [HttpGet]
    public async Task<TransportOpdrachtResult.Index> GetIndex([FromQuery] TransportOpdrachtRequest.Index request)
    {
        return await service.GetIndexAsync(request);
    }

    [SwaggerOperation("Haalt een Transportopdracht op aan de hand van zijn Id")]
    [HttpGet("{id}")]
    public async Task<TransportOpdrachtDto.Detail> GetDetail(int id)
    {
        return await service.GetDetailAsync(id);
    }

    [SwaggerOperation("Maakt een nieuwe transportopdracht aan.")]
    [HttpPost]
    [Authorize(Roles ="Lader")]
    public async Task<IActionResult> Create(TransportOpdrachtDto.Mutate model)
    {
        var id = await service.CreateAsync(model);
        return CreatedAtAction(nameof(Create), id);
    }

    [SwaggerOperation("Past een bestaande transportopdracht aan.")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(int id, TransportOpdrachtDto.Mutate model)
    {
        await service.EditAsync(id, model);
        return NoContent();
    }
}
