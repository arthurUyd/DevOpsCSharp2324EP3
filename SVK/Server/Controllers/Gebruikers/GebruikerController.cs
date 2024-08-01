using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers.Gebruikers;

[ApiController]
[Route("api/[controller]")]
public class GebruikerController : ControllerBase
{

    public IActionResult Index()
    {
        return View();
    }
}
