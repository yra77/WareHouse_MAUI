

using android = Android;
using WareHouse_MAUI.Enums;
using WareHouse_MAUI.Services.Interfaces;
using Android.Views;
using Android.OS;


namespace WareHouse_MAUI.Platforms.Android.Services
{
    internal class ChangeThemeService : IChangeThemeService
    {

        public void SetTheme(ThemeEnum mode)
        {
            if (mode == ThemeEnum.Dark)
            {
                //// The thin bar at top of Android screen.
                MainActivity.Inst.Window.SetStatusBarColor(android.Graphics.Color.ParseColor("#1B252F"));
                if (Build.VERSION.SdkInt >= BuildVersionCodes.R)
                {
                    MainActivity.Inst.Window.InsetsController?.
                         SetSystemBarsAppearance((int)WindowInsetsControllerAppearance.None,//LightStatusBars,
                         (int)WindowInsetsControllerAppearance.None);// LightStatusBars);
                }
                else
                {
                    MainActivity.Inst.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.Visible;
                }
            }
            else if (mode == ThemeEnum.Light)
            {
                MainActivity.Inst.Window.SetStatusBarColor(android.Graphics.Color.ParseColor("#F8F9FA"));
                if (Build.VERSION.SdkInt >= BuildVersionCodes.R)
                {
                    MainActivity.Inst.Window.InsetsController?.
                        SetSystemBarsAppearance((int)WindowInsetsControllerAppearance.LightStatusBars,
                        (int)WindowInsetsControllerAppearance.LightStatusBars);
                }
                else
                {
                    MainActivity.Inst.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LightStatusBar;
                }
            }
        }
    }
}
