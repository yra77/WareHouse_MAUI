

using WareHouse_MAUI.Services.Interfaces;
using WareHouse_MAUI.ViewModels;

using a = Microsoft.Maui.ApplicationModel;

using Android.App;
using Android.Views;
using Android.Widget;
using android = Android;
using Android.Graphics;
using System.Diagnostics;


namespace WareHouse_MAUI.Platforms.Android.Services.Permissions
{
    internal class Android_Services : ICheck_AndroidServives
    {

        private ChangeNethwork _callback;
        private bool _state;


        public Android_Services()
        {
            _state = true;
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }


        ~Android_Services()
        {
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }


        private async void IsServerConnect()
        {

            Uri uri = new Uri(Constants.ServerPaths.PING_URL);// $"http://192.168.0.105:5249/api/Ping");
            var _client = new HttpClient();

            while (true)
            {

                try
                {
                    HttpResponseMessage response = await _client.GetAsync(uri);

                    if (response.IsSuccessStatusCode)
                    {
                        if (!_state)
                        {
                            Print(WareHouse_MAUI.Resources.Strings.Resource.ConRestored, "#228B22");
                            _state = true;
                            _callback(_state);
                        }
                    }
                    else
                    {
                        if (_state)
                        {
                            Print(WareHouse_MAUI.Resources.Strings.Resource.NotServer, "#DC143C");
                            _state = false;
                            _callback(_state);
                        }
                    }
                }
                catch (Exception)
                {
                    if (_state)
                    {
                        Print(WareHouse_MAUI.Resources.Strings.Resource.NotServer, "#DC143C");
                        _state = false;
                        _callback(_state);
                    }
                }

                await Task.Delay(10000);
            }
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {

            if (e.NetworkAccess == NetworkAccess.ConstrainedInternet)
            {
                _state = false;
                Print(WareHouse_MAUI.Resources.Strings.Resource.ConLimited, "#DC143C");
                _callback(_state);
            }

            if (e.NetworkAccess != NetworkAccess.Internet)
            {
                _state = false;
                Print(WareHouse_MAUI.Resources.Strings.Resource.Offline, "#DC143C");
                _callback(_state);
            }

            if (!_state &&
                e.NetworkAccess == NetworkAccess.Internet &&
                e.NetworkAccess != NetworkAccess.ConstrainedInternet)
            {
                _state = true;
                Print(WareHouse_MAUI.Resources.Strings.Resource.ConRestored, "#228B22");

                _callback(_state);
            }
        }

        public async Task PermissionsStatus()
        {

            PermissionStatus status =
                      await a.Permissions.CheckStatusAsync<a.Permissions.Camera>();

            if (status == PermissionStatus.Denied)
            {
                await a.Permissions.RequestAsync<a.Permissions.Camera>();
                Print(WareHouse_MAUI.Resources.Strings.Resource.CamPermissions, "#DC143C");
                _state = false;
                _callback(_state);
            }
            else
            {
                Print(WareHouse_MAUI.Resources.Strings.Resource.ConRestored, "#228B22");
                _state = true;
                _callback(_state);
            }
        }

        public async Task CheckServices(ChangeNethwork callback = null)
        {

            if (callback != null)
            {
                _callback = callback;
            }

            if (Connectivity.NetworkAccess == NetworkAccess.None
                || Connectivity.NetworkAccess == NetworkAccess.Unknown
                || Connectivity.NetworkAccess == NetworkAccess.Local)
            {
                _state = false;
                Print(WareHouse_MAUI.Resources.Strings.Resource.Offline, "#DC143C");
                _callback(_state);
            }

            await Task.Run(IsServerConnect);

        }

        private static void Print(string str, string color)
        {

            MainActivity.Inst.RunOnUiThread(() =>
            {
                if (color == "#228B22")
                {
                    MainActivity.Inst.Window.SetStatusBarColor(android.Graphics.Color.ParseColor("#F8F9FA"));
                }
                else
                {
                    MainActivity.Inst.Window.SetStatusBarColor(android.Graphics.Color.ParseColor(color));
                }

                Toast toast = Toast.MakeText(MainActivity.Inst, str, ToastLength.Long);
                toast.View.Background.SetColorFilter(android.Graphics.Color.ParseColor(color), PorterDuff.Mode.SrcIn);
                toast.SetGravity(GravityFlags.Center, 0, 0);
                toast.Show();
            });
        }

    }
}
