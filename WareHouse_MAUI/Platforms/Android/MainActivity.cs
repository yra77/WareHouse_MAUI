

using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;


namespace WareHouse_MAUI;


[Activity(ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/Maui.SplashTheme",
          MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize |
          ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout |
          ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{

    public static MainActivity Inst { get; set; }
    private long _milliSeconds = 0;


    protected async override void OnCreate(Android.OS.Bundle bundle)
    {
        base.OnCreate(bundle);

        Inst = this;

      //  Window.SetSoftInputMode(SoftInput.AdjustNothing);//клавиатура не сдвигает контент

        //WindowInsetsControllerCompat windowInsetsController =
        //    WindowCompat.GetInsetsController(Window, Window.DecorView);
        //windowInsetsController.Hide(WindowInsetsCompat.Type.SystemBars());


        if (Build.VERSION.SdkInt >= BuildVersionCodes.R)
        {
            Window.InsetsController?.SetSystemBarsAppearance(
                          (int)WindowInsetsControllerAppearance.LightStatusBars,
                          (int)WindowInsetsControllerAppearance.LightStatusBars);
        }
        else
        {
            Window.DecorView.SystemUiVisibility =
                          (StatusBarVisibility)SystemUiFlags.LightStatusBar;
        }
        Platform.Init(this, bundle);

        await Permissions.RequestAsync<Permissions.StorageWrite>();
        await Permissions.RequestAsync<Permissions.Camera>();
    }

    public override void OnRequestPermissionsResult(int requestCode,
                                                          string[] permissions,
                                                          Permission[] grantResults)
    {
        Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
    }


    public override void OnBackPressed()
    {
        var navigation = Microsoft.Maui.Controls.Application.Current?.MainPage?.Navigation;

        if (navigation is null ||
            navigation.NavigationStack.Count > 1 ||
            navigation.ModalStack.Count > 0)
        {
            base.OnBackPressed();
        }
        // else
        // { 
        const int delay = 1000;
        if (_milliSeconds + delay > DateTimeOffset.UtcNow.ToUnixTimeMilliseconds())
        {
            FinishAndRemoveTask();
            Process.KillProcess(Process.MyPid());
        }
        else
        {
            //change status bar color
            MainActivity.Inst.Window.SetStatusBarColor(Android.Graphics.Color.ParseColor("#F8F9FA"));
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

            //view message
            Toast toast = Toast.MakeText(this, "Press back again to exit", ToastLength.Short);
            toast.SetGravity(GravityFlags.Center, 0, 0);
            toast.Show();

            _milliSeconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }
        // }
    }
}
