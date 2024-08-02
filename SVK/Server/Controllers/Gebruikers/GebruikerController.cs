using Microsoft.AspNetCore.Mvc;
using NHibernate.Hql.Ast.ANTLR.Util;
using Shared.Gebruikers;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.Gebruikers;

[ApiController]
[Route("api/[controller]")]
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

    [SwaggerOperation("Haalt een gebruiker op aan de hand van zijn Id.")]
    [HttpGet("{id}")]
    public async Task<GebruikerDto.Detail> GetDetail(int id)
    {
        return await gebruikerService.GetDetailAsync(id);
    }

    [SwaggerOperation("Maakt een gebruiker aan.")]
    [HttpPost]  
    public async Task<IActionResult> Create(GebruikerDto.Mutate model)
    {
        var customerid = await gebruikerService.CreateAsync(model);
        return CreatedAtAction(nameof(Create), customerid);
    }


}
