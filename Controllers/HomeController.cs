using Microsoft.AspNetCore.Mvc;

namespace ClickME.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
