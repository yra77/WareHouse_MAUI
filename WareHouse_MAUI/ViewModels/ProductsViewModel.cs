﻿

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
    internal class ProductsViewModel : BaseViewModel, INavigatedAware
    {


        private List<Product> _productList2;


        public ProductsViewModel(ILocalizationManager localizationManager,
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

        private List<Product> _productList;
        public List<Product> ProductList
        {
            get => _productList;
            set => SetProperty(ref _productList, value);
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
        public DelegateCommand AddProductClick => new DelegateCommand(AddProduct);
        public DelegateCommand RefreshList => new DelegateCommand(Refresh_List);
        public DelegateCommand<string> Column => new DelegateCommand<string>(ColumnClick);
        public DelegateCommand<Product> ItemSelect => new DelegateCommand<Product>(ItemSelectClick);

        #endregion


        private async void ItemSelectClick(Product obj)
        {
            var parameters = new NavigationParameters
                                {
                                  { "item", obj}
                                };
            await _navigationService.NavigateAsync("AddUpdateProduct", parameters);
        }

        private async void AddProduct()
        {
          await _navigationService.NavigateAsync("AddUpdateProduct");
        }

        private void ColumnClick(string columnName)
        {
            switch (columnName)
            {
                case "Id":
                    ProductList = ProductList.AsParallel().OrderBy(x => x.Id).ToList();
                    break;
                case "Name":
                    ProductList = ProductList.AsParallel().OrderBy(x => x.Name).ToList();
                    break;
                case "Code":
                    ProductList = ProductList.AsParallel().OrderBy(x => x.Code).ToList();
                    break;
            }
            RaisePropertyChanged("ProductList");
        }

        private async void Refresh_List()
        {
           // IsRefresh = true;
            var list = await _dataService.GetDataAsync<Product>();
            ProductList = new List<Product>(list);
            _productList2 = new List<Product>(list);
            RaisePropertyChanged("ProductList");
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
                            ProductList = _productList2.AsParallel().Where(item => item.Name.ToLower().Contains(SearchText.ToLower())
                                            || item.Id.ToString().Contains(SearchText)
                                            || item.Code.Contains(SearchText)).ToList();
                        }
                        else
                        {
                            ProductList = _productList2;
                        }

                        RaisePropertyChanged("ProductList");
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
