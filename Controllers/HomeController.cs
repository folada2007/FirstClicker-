using ClickME.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClickME.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> ClickChecker() 
    {
        var user = HttpContext.User.Identity.Name;
        var DbUser = await _context.users.FirstOrDefaultAsync(c => c.Name == user);
        if (DbUser != null) 
        {
            DbUser.Count++;
            await _context.SaveChangesAsync();
            return Json(new { clickCount = DbUser.Count });
        }
        return Json(new { message = "ױַ עû קונע" });
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult GetNewImage() 
    {
        string newImageUrl = "/img/kulagin2frame.png";
        return Json(newImageUrl);
    }
}
