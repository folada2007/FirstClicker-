using ClickME.Models;
using ClickME.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClickME.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IDbSave _dbsave;
        private readonly ApplicationDbContext _context;
        private readonly IUserAuthentication _userAuthentication;
        public RegistrationController(IDbSave dbsave, ApplicationDbContext context,IUserAuthentication userAuthentication)
        {
            _dbsave = dbsave;
            _context = context;
            _userAuthentication = userAuthentication;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LogOut() 
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }

            return RedirectToAction("Index","Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid) 
            {
                var principal = await _userAuthentication.CreateClaimPrincipalAsync(login);
                if (principal != null) 
                {
                    await _userAuthentication.SignInAsync(principal);
                    return RedirectToAction("Index","Home");
                }
                ModelState.AddModelError(string.Empty, "Неверный логин или пароль проверьте ваши данные или пройдите регистрацию");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Check(Registration registration) 
        {
            if (ModelState.IsValid) 
            {
                await _dbsave.SaveAsync(registration);
                return RedirectToAction("Login", "Registration");
            }
            return View("Index");
        }
    }
}
