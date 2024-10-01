using ClickME.Models;
using System.Security.Claims;

namespace ClickME.Services.Interfaces
{
    public interface IUserAuthentication
    {
        Task<ClaimsPrincipal> CreateClaimPrincipalAsync(Login login);
        Task SignInAsync(ClaimsPrincipal claimsPrincipal);
    }
}
