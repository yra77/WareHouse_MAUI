

namespace WareHouse_MAUI.Services.SettingsManager
{
    public interface ISettingsManager
    {
        string Email { get; set; }
        string Password { get; set; }
        string Name { get; set; }
        string Language { get; set; }
        string Tel { get; set; }
        string Address { get; set; }
        int State { get; set; }// 0 - default, 1 - changes without the internet 
        DateTime LastUpdate_DB { get; set; }
    }
}
