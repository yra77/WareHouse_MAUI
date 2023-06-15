

using WareHouse_MAUI.Models;
using WareHouse_MAUI.Services.Interfaces;
using WareHouse_MAUI.Services.AndroidMedia;
using WareHouse_MAUI.Services.Localization;
using WareHouse_MAUI.Services.DataServices;
using WareHouse_MAUI.Services.VerifyService;
using WareHouse_MAUI.Services.SettingsManager;

using System.ComponentModel;

namespace WareHouse_MAUI.ViewModels
{
    internal class ShippersViewModel : BaseViewModel, INavigationAware
    {


        private List<Shipper> _shipperList2;


        public ShippersViewModel(ILocalizationManager localizationManager,
                             INavigationService navigationService,
                             ICheck_AndroidServives checkAndroid,
                             ISettingsManager settingsManager,
                             IVerifyInputService verifyInput,
                             IUnfocusedEntry unfocusedEntry,
                             IAndroidMedia androidMedia,
                             IPrintMessage printMessage,
                             IDataService dataService)
        {
            _localizationManager = localizationManager;
            _navigationService = navigationService;
            _settingsManager = settingsManager;
            _unfocusedEntry = unfocusedEntry;
            _checkAndroid = checkAndroid;
            _printMessage = printMessage;
            _androidMedia = androidMedia;
            _verifyInput = verifyInput;
            _dataService = dataService;
        }


        #region property

        private List<Shipper> _listShipper;
        public List<Shipper> ListShipper
        {
            get => _listShipper;
            set => SetProperty(ref _listShipper, value);
        }


        private bool _isValidInput;
        public bool IsValidInput
        {
            get => _isValidInput;
            set => SetProperty(ref _isValidInput, value);
        }


        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }


        private bool _isRefresh;
        public bool IsRefresh
        {
            get => _isRefresh;
            set => SetProperty(ref _isRefresh, value);
        }


        public DelegateCommand UnfocusedCommand => new DelegateCommand(Unfocused_Entry);
        public DelegateCommand BackClick => new DelegateCommand(GoBack);
        public DelegateCommand AddShipperClick => new DelegateCommand(AddShipper);
        public DelegateCommand RefreshList => new DelegateCommand(Refresh_List);
        public DelegateCommand<string> Column => new DelegateCommand<string>(ColumnClick);
        public DelegateCommand<Shipper> ItemSelect => new DelegateCommand<Shipper>(ItemSelectClick);

        #endregion


        private async void ItemSelectClick(Shipper obj)
        {
            var parameters = new NavigationParameters
                                {
                                  { "item", obj}
                                };
            await _navigationService.NavigateAsync("AddUpdateShipper", parameters);
        }

        private async void AddShipper()
        {
            await _navigationService.NavigateAsync("AddUpdateShipper");
        }

        private void ColumnClick(string columnName)
        {
            switch (columnName)
            {
                case "Id":
                    ListShipper = ListShipper.AsParallel().OrderBy(x => x.Id).ToList();
                    break;
                case "Name":
                    ListShipper = ListShipper.AsParallel().OrderBy(x => x.Name).ToList();
                    break;
                case "Phone":
                    ListShipper = ListShipper.AsParallel().OrderBy(x => x.Phone).ToList();
                    break;
            }
            RaisePropertyChanged("ListShipper");
        }

        private async void Refresh_List()
        {
            // IsRefresh = true;
            var list = await _dataService.GetDataAsync<Shipper>();
            ListShipper = new List<Shipper>(list);
            _shipperList2 = new List<Shipper>(list);
            RaisePropertyChanged("ListShipper");
            IsRefresh = false;
        }

        private async void GoBack()
        {
            await _navigationService.GoBackAsync();
        }

        private void Unfocused_Entry()
        {
            _unfocusedEntry.HideKeyboard();
        }


        #region interface implement

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {

                case "SearchText":
                    if (SearchText != null)
                    {
                        if (SearchText.Length > 0)
                        {
                            ListShipper = _shipperList2.AsParallel().Where(item => item.Name.ToLower().Contains(SearchText.ToLower())
                                            || item.Id.ToString().Contains(SearchText)
                                            || item.Phone.Contains(SearchText)).ToList();
                        }
                        else
                        {
                            ListShipper = _shipperList2;
                        }

                        RaisePropertyChanged("ListShipper");
                    }
                    break;
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            Unfocused_Entry();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            // Unfocused_Entry();
            SearchText = "";
            IsVisibleIndicator = true;
            Refresh_List();
            IsVisibleIndicator = false;
        }
        #endregion
    }
}
