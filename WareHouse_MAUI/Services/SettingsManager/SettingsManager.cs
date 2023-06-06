

namespace WareHouse_MAUI.Services.SettingsManager
{
    class SettingsManager : ISettingsManager
    {
        public string Email 
        { 
            get => Preferences.Get(nameof(Email), null); 
            set => Preferences.Set(nameof(Email), value); 
        }

        public string Password
        {
            get => Preferences.Get(nameof(Password), null);
            set => Preferences.Set(nameof(Password), value);
        }
        public string Name
        {
            get => Preferences.Get(nameof(Name), null);
            set => Preferences.Set(nameof(Name), value);
        }

        public string Language
        {
            get => Preferences.Get(nameof(Language), "en");
            set => Preferences.Set(nameof(Language), value);
        }

        public string Tel
        {
            get => Preferences.Get(nameof(Tel), null);
            set => Preferences.Set(nameof(Tel), value);
        }

        public string Address
        {
            get => Preferences.Get(nameof(Address), null);
            set => Preferences.Set(nameof(Address), value);
        }

        public DateTime LastUpdate_DB
        {
            get => Preferences.Get(nameof(LastUpdate_DB), DateTime.Now);
            set => Preferences.Set(nameof(LastUpdate_DB), value);
        }
        public int State
        {
            get => Preferences.Get(nameof(State), 0);
            set => Preferences.Set(nameof(State), value);
        }
    }
}
