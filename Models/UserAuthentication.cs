using ClickME.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ClickME.Models
{
    public class UserAuthentication:IUserAuthentication
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAuthentication(ApplicationDbContext context, PasswordHasher<User> passwordHasher, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ClaimsPrincipal> CreateClaimPrincipalAsync(Login login) 
        {
            var Users = await _context.users.FirstOrDefaultAsync(c => c.Name == login.Name);
            if (Users != null)
            {
                var userPassword = _passwordHasher.VerifyHashedPassword(Users, Users.HashPassword, login.Password);
                if (userPassword == PasswordVerificationResult.Success) 
                {
                    var count = Users.Count;
                    var claim = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,login.Name),
/*                        new Claim("Count",count.ToString())*/
                    };

                    var claimsIdentity = new ClaimsIdentity(claim,CookieAuthenticationDefaults.AuthenticationScheme);
                    return new ClaimsPrincipal(claimsIdentity);
                }
            }
            return null;
            
        }

        public async Task SignInAsync(ClaimsPrincipal claimsPrincipal)
        {
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(60)
                });
        }
    }
}
