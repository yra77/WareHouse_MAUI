

using WareHouse_MAUI.Models;
using WareHouse_MAUI.Views.Regions;
using WareHouse_MAUI.Services.Interfaces;
using WareHouse_MAUI.Services.AccessRoles;
using WareHouse_MAUI.Services.AndroidMedia;
using WareHouse_MAUI.Services.Localization;
using WareHouse_MAUI.Services.DataServices;
using WareHouse_MAUI.Services.VerifyService;
using WareHouse_MAUI.Services.SettingsManager;

using System.Collections.ObjectModel;


namespace WareHouse_MAUI.ViewModels
{
    internal class HomeViewModel : BaseViewModel, INavigatedAware
    {


        private bool _isMenuOpen;
        private bool _isOpen;


        public HomeViewModel(ILocalizationManager localizationManager,
                             INavigationService navigationService,
                             ICheck_AndroidServives checkAndroid,
                             ISettingsManager settingsManager,
                             IVerifyInputService verifyInput,
                             IChangeThemeService changeTheme,
                             IUnfocusedEntry unfocusedEntry,
                             IRegionManager regionManager,
                             IAndroidMedia androidMedia,
                             IPrintMessage printMessage,
                             IDataService dataService,
                             IAccessRole accessRole)
        {
            _localizationManager = localizationManager;
            _navigationService = navigationService;
            _settingsManager = settingsManager;
            _unfocusedEntry = unfocusedEntry;
            _regionManager = regionManager;
            _checkAndroid = checkAndroid;
            _printMessage = printMessage;
            _androidMedia = androidMedia;
            _changeTheme = changeTheme;
            _verifyInput = verifyInput;
            _dataService = dataService;
            _accessRole = accessRole;


            TraslateX = 0;
            ScaleBaseContent = 1;
            RotateBaseContent = 0;
            _isMenuOpen = false;
            _isOpen = false;
          
            LogoutText = "";
        }


        #region property


        private double _traslateX;
        public double TraslateX
        {
            get => _traslateX;
            set => SetProperty(ref _traslateX, value);
        }


        private double _scaleBaseContent;
        public double ScaleBaseContent
        {
            get => _scaleBaseContent;
            set => SetProperty(ref _scaleBaseContent, value);
        }


        private double _rotateBaseContent;
        public double RotateBaseContent
        {
            get => _rotateBaseContent;
            set => SetProperty(ref _rotateBaseContent, value);
        }


        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }


        private byte[] _photoPath;
        public byte[] PhotoPath
        {
            get => _photoPath;
            set => SetProperty(ref _photoPath, value);
        }


        private string _logoutText;
        public string LogoutText
        {
            get => _logoutText;
            set => SetProperty(ref _logoutText, value);
        }


        private bool _highestPermission;
        public bool HighestPermission
        {
            get => _highestPermission;
            set => SetProperty(ref _highestPermission, value);
        }


        private bool _mediumPermission;
        public bool MediumPermission
        {
            get => _mediumPermission;
            set => SetProperty(ref _mediumPermission, value);
        }


        private bool _lowPermission;
        public bool LowPermission
        {
            get => _lowPermission;
            set => SetProperty(ref _lowPermission, value);
        }


        private bool _lowestPermission;
        public bool LowestPermission
        {
            get => _lowestPermission;
            set => SetProperty(ref _lowestPermission, value);
        }


        public DelegateCommand UnfocusedCommand => new DelegateCommand(Unfocused_Entry);
        public DelegateCommand HamburgerClick => new DelegateCommand(Hamburger_Click);
        public DelegateCommand SettingsClick => new DelegateCommand(Settings_Click);
        public DelegateCommand ScanQrCodeClick => new DelegateCommand(ScanQrCodeClick_Click);
        public DelegateCommand Logout => new DelegateCommand(LogoutClick);
        public DelegateCommand<string> GoToTable => new DelegateCommand<string>(GoTo);

        #endregion


        private async void GoTo(string path)
        {
            await _navigationService.NavigateAsync(path);
        }

        private async void LogoutClick()
        {
           _settingsManager.Email = "";
          _settingsManager.Password = "";
            await _navigationService.NavigateAsync("MainPage");
        }

        private async void Settings_Click()
        {
            await _navigationService.NavigateAsync("Settings");
        }

        private async void ScanQrCodeClick_Click()
        {
            if (LowPermission)
            {
                await _navigationService.NavigateAsync("QrCodeScan");
            }
        }

        private async void Hamburger_Click()
        {
            Unfocused_Entry();

            if (!_isMenuOpen)
            {
                _changeTheme.SetTheme(Enums.ThemeEnum.Dark);
                Animation(0, 200, 1);
                _isMenuOpen = true;
            }
            else
            {
                Animation(199, -1, 1);
                _isMenuOpen = false;
                await Task.Delay(300);
                _changeTheme.SetTheme(Enums.ThemeEnum.Light);
            }
        }

        private async void Animation(int start, int end, int duration)
        {
            if (start < end)
            {
                for (int i = start; i < end; i += 10)
                {
                    TraslateX = i;
                    ScaleBaseContent = 1.0 - (double)i / 1000;
                    RotateBaseContent = -(double)i / 20;
                    await Task.Delay(duration);
                }
            }
            else
            {
                for (int i = start; i >= end; i -= 10)
                {
                    TraslateX = i;
                    ScaleBaseContent = 1.0 - (double)i / 1000;
                    RotateBaseContent = -(double)i / 20;
                    await Task.Delay(duration);
                }
            }
        }

        private void Unfocused_Entry()
        {
            _unfocusedEntry.HideKeyboard();
        }


        #region implement interface

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            _isMenuOpen = false;
            _changeTheme.SetTheme(Enums.ThemeEnum.Light);
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            _accessRole.GetAccessPermission(out _highestPermission,
                                              out _mediumPermission,
                                              out _lowPermission,
                                              out _lowestPermission);

            LogoutText = _employee.Role + " " + _localizationManager["Logout"];
          
            if (!_isOpen)
            {
                _isOpen = true;
            }

            _isMenuOpen = false;
            _changeTheme.SetTheme(Enums.ThemeEnum.Light);

            if (_employee.Photo != null)
            {
                PhotoPath = _employee.Photo;
            }


            if (LowPermission)
            {
               
            }
            else
            {
                _printMessage.ViewMessage(_localizationManager["PermissionMsg"]);
            }
        }

        #endregion
    }
}
