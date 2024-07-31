using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;;

public class GebruikerController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
