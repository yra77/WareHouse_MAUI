

using System.Globalization;


namespace WareHouse_MAUI.Services.Localization
{
    internal interface ILocalizationManager
    {
        public string this[string resourceKey] { get; }
        CultureInfo GetCultureInfo { get; }
        void SetCulture(CultureInfo culture);
    }
}
