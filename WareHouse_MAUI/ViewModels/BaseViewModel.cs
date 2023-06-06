

using WareHouse_MAUI.Models;
using WareHouse_MAUI.Services.Auth;
using WareHouse_MAUI.Services.Interfaces;
using WareHouse_MAUI.Services.AccessRoles;
using WareHouse_MAUI.Services.AndroidMedia;
using WareHouse_MAUI.Services.Localization;
using WareHouse_MAUI.Services.VerifyService;
using WareHouse_MAUI.Services.SettingsManager;

using System.Collections.ObjectModel;


namespace WareHouse_MAUI.ViewModels
{

    public delegate void ChangeNethwork(bool state);

    internal class BaseViewModel : BindableBase
    {


        protected static bool _stateNethwork;
        protected static ObservableCollection<Product> _staticList;
        protected static Employee _employee;

        protected bool _isPressed;

        protected ILocalizationManager _localizationManager;
        protected INavigationService _navigationService;
        protected ICheck_AndroidServives _checkAndroid;
        protected ISettingsManager _settingsManager;
        protected IVerifyInputService _verifyInput;
        protected IChangeThemeService _changeTheme;
        protected IUnfocusedEntry _unfocusedEntry;
        protected IRegionManager _regionManager;
        protected IAndroidMedia _androidMedia;
        protected IPrintMessage _printMessage;
        protected IAccessRole _accessRole;
        protected IAuth _auth;

        protected ChangeNethwork _changeNethwork;


        static BaseViewModel()
        {
            _stateNethwork = true;
            _staticList = new ObservableCollection<Product>();
            _employee = new Employee();
        }

        public BaseViewModel()
        {
            _changeNethwork = IsNethwork;
            _isPressed = false;
        }


        #region public property


        private bool _isVisibleIndicator;
        public bool IsVisibleIndicator
        {
            get => _isVisibleIndicator;
            set => SetProperty(ref _isVisibleIndicator, value);
        }


        #endregion


        protected virtual void IsNethwork(bool state)
        {
            _stateNethwork = state;
        }
    }
}
