

using WareHouse_MAUI.Models;


namespace WareHouse_MAUI.Services.Auth
{
    public interface IAuth
    {
        Task<(bool, (string, Employee))> AuthAsync(string password, string email);
    }
}
