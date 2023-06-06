

using WareHouse_MAUI.Services.Interfaces;
using Android.Content;
using Android.Views.InputMethods;
using android = Android;


namespace WareHouse_MAUI.Platforms.Android.Services
{
    internal class UnfocusedEntry : IUnfocusedEntry
    {
        public void HideKeyboard()
        {
            var context = android.App.Application.Context;
            var inputMethodManager = context.GetSystemService(Context.InputMethodService) as InputMethodManager;
            if (inputMethodManager != null)
            {
                var activity = Platform.CurrentActivity;
                var token = activity.CurrentFocus?.WindowToken;
                inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);
                activity.Window.DecorView.ClearFocus();
            }

        }
    }
}
