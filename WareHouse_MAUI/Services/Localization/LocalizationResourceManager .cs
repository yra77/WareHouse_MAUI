

using System.ComponentModel;
using System.Globalization;


namespace WareHouse_MAUI.Services.Localization
{
    internal class LocalizationResourceManager : INotifyPropertyChanged, ILocalizationManager
    {


        public LocalizationResourceManager()
        {
            instance = this;
            WareHouse_MAUI.Resources.Strings.Resource.Culture = CultureInfo.CurrentCulture;
        }



        private static LocalizationResourceManager instance = null;
        public static LocalizationResourceManager Instance { get { return instance; } }

        public CultureInfo GetCultureInfo{ get => WareHouse_MAUI.Resources.Strings.Resource.Culture; }

        public string this[string resourceKey] =>
                           WareHouse_MAUI.Resources.Strings.Resource.ResourceManager.GetString(resourceKey,
                           WareHouse_MAUI.Resources.Strings.Resource.Culture);// ?? Array.Empty<byte>();

       

        public void SetCulture(CultureInfo culture)
        {
            WareHouse_MAUI.Resources.Strings.Resource.Culture = culture;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
