using ClickME.Models;

namespace ClickME.Services.Interfaces
{
    public interface IDbSave
    {
        Task SaveAsync(Registration registration);
    }
}
