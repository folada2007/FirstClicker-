using System.Diagnostics;
using ClickME.Models;
using ClickME.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClickME.Controllers;

public class HomeController : Controller
{
    private readonly IDbSave _dbSave;
    private readonly ApplicationDbContext _context;
    public HomeController(IDbSave dbSave, ApplicationDbContext context)
    {
        _dbSave = dbSave;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Check(Registration registration) 
    {
        if (ModelState.IsValid) 
        {
            await _dbSave.SaveAsync(registration);
            ViewBag.Info = _context.users.ToList();
            return View();
        }

        return View("Index");
    }
}
