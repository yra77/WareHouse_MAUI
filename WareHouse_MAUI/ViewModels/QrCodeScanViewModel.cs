

using WareHouse_MAUI.Models;
using WareHouse_MAUI.Views.Regions;
using WareHouse_MAUI.Services.Interfaces;
using WareHouse_MAUI.Services.AccessRoles;
using WareHouse_MAUI.Services.AndroidMedia;
using WareHouse_MAUI.Services.Localization;
using WareHouse_MAUI.Services.VerifyService;
using WareHouse_MAUI.Services.SettingsManager;

using BarcodeScanner.Mobile;
using System.Collections.ObjectModel;


namespace WareHouse_MAUI.ViewModels
{
    internal class QrCodeScanViewModel : BaseViewModel, INavigatedAware
    {
        public QrCodeScanViewModel(ILocalizationManager localizationManager,
                                   INavigationService navigationService,
                             ICheck_AndroidServives checkAndroid,
                             ISettingsManager settingsManager,
                             IVerifyInputService verifyInput,
                             IUnfocusedEntry unfocusedEntry,
                             IRegionManager regionManager,
                             IAndroidMedia androidMedia,
                             IPrintMessage printMessage,
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
            _verifyInput = verifyInput;
            _accessRole = accessRole;
        }


        #region property

        private ObservableCollection<Product> _productList;
        public ObservableCollection<Product> ProductList
        {
            get => _productList;
            set => SetProperty(ref _productList, value);
        }


        private bool _isOnTorch;
        public bool IsOnTorch
        {
            get => _isOnTorch;
            set => SetProperty(ref _isOnTorch, value);
        }


        private bool _isScanning;
        public bool IsScanning
        {
            get => _isScanning;
            set => SetProperty(ref _isScanning, value);
        }


        private string _qrCodeResult;
        public string QrCodeResult
        {
            get => _qrCodeResult;
            set => SetProperty(ref _qrCodeResult, value);
        }


        public DelegateCommand BackClick => new DelegateCommand(Back_Click);
        public DelegateCommand SettingsClick => new DelegateCommand(Settings_Click);
        public DelegateCommand ScanQrCodeClick => new DelegateCommand(ScanQrCodeClick_Click);
        public DelegateCommand<OnDetectedEventArg> DetectBarCode => new DelegateCommand<OnDetectedEventArg>(Barcodes_Detected);
        public DelegateCommand IsTorchBtn => new DelegateCommand(IsTorchClick);
        public DelegateCommand UnfocusedCommand => new DelegateCommand(Unfocused_Entry);

        #endregion


        private async void Settings_Click()
        {
            await _navigationService.NavigateAsync("Settings");
        }

        private void ScanQrCodeClick_Click()
        {
            _regionManager.RegisterViewWithRegion("QrCode_Region", typeof(QrCodeRegion));
            // _regionManager.RequestNavigate("QrCode_Region", "QrCodeRegion");
        }

        private async void Back_Click()
        {
            Unfocused_Entry();
            await _navigationService.GoBackAsync();
        }

            private void IsTorchClick()
        {
            if (IsOnTorch)
            {
                IsOnTorch = false;
            }
            else
            {
                IsOnTorch = true;
            }
        }

        private void Barcodes_Detected(OnDetectedEventArg e)
        {
            List<BarcodeResult> obj = e.BarcodeResults;

            string result = string.Empty;
            for (int i = 0; i < obj.Count; i++)
            {
                result += $"Format : {obj[i].BarcodeFormat}, Type : {obj[i].BarcodeType}, Value : {obj[i].DisplayValue}{Environment.NewLine}";
            }

            _androidMedia.GetBeep();
            IsScanning = false;
            QrCodeResult = result;
        }

        private void Unfocused_Entry()
        {
            _unfocusedEntry.HideKeyboard();
        }


        #region interface implement

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            IsScanning = false;
            IsOnTorch = false;
        }

        #endregion
    }
}
