

using WareHouse_MAUI.Services.Interfaces;
using android = Android;
using Android.Graphics;
using Android.Widget;
using Android.Views;
using Android.App;


namespace WareHouse_MAUI.Platforms.Android.Services
{
    internal class PrintMessage : Activity, IPrintMessage
    {
        public void ViewMessage(string msg, string color = "#DC143C")
        {

            MainActivity.Inst.RunOnUiThread(() =>
            {
                Toast toast = Toast.MakeText(MainActivity.Inst, msg, ToastLength.Long);
                toast.View.Background.SetColorFilter(android.Graphics.Color.ParseColor(color), PorterDuff.Mode.SrcIn);
                toast.SetGravity(GravityFlags.Center, 0, 0);
                toast.Show();
            });
        }
    }
}
