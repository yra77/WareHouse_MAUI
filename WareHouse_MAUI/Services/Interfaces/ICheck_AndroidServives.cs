

using WareHouse_MAUI.ViewModels;


namespace WareHouse_MAUI.Services.Interfaces
{
    public interface ICheck_AndroidServives
    {
        Task CheckServices(ChangeNethwork callback = null);
        Task PermissionsStatus();
    }
}
