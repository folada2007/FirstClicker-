using ClickME.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ClickME.Models
{
    public class DbSave:IDbSave
    {
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly ApplicationDbContext _context;
        public DbSave(PasswordHasher<User> passwordHasher, ApplicationDbContext context)
        {
            _passwordHasher = passwordHasher;
            _context = context;
        }

        public async Task SaveAsync(Registration registration)
        {
            var user = new User 
            {
                Name = registration.Name,
                Email = registration.Email,
            };

            user.HashPassword = _passwordHasher.HashPassword(user,registration.Password);
            _context.users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
