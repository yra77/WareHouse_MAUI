

using WareHouse_MAUI.Models;
using WareHouse_MAUI.Services.Interfaces;
using WareHouse_MAUI.Services.AndroidMedia;
using WareHouse_MAUI.Services.Localization;
using WareHouse_MAUI.Services.DataServices;
using WareHouse_MAUI.Services.VerifyService;
using WareHouse_MAUI.Services.SettingsManager;
using WareHouse_MAUI.Platforms.Android.Services;

using System.ComponentModel;

using Prism.Navigation;
using Prism.Regions.Navigation;


namespace WareHouse_MAUI.ViewModels
{
    internal class AddUpdateProductViewModel : BaseViewModel, INavigationAware
    {


        public AddUpdateProductViewModel(ILocalizationManager localizationManager,
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

        private Product _product;
        public Product Product
        {
            get => _product;
            set => SetProperty(ref _product, value);
        }


        private List<ProductType> _listType;
        public List<ProductType> ListType
        {
            get => _listType;
            set => SetProperty(ref _listType, value);
        }


        private List<Categories> _listCategory;
        public List<Categories> ListCategory
        {
            get => _listCategory;
            set => SetProperty(ref _listCategory, value);
        }


        private List<Shipper> _listShipper;
        public List<Shipper> ListShipper
        {
            get => _listShipper;
            set => SetProperty(ref _listShipper, value);
        }


        private bool _isDeleteVisible;
        public bool IsDeleteVisible
        {
            get => _isDeleteVisible;
            set => SetProperty(ref _isDeleteVisible, value);
        }


        private bool _isValidInput;
        public bool IsValidInput
        {
            get => _isValidInput;
            set => SetProperty(ref _isValidInput, value);
        }


        private string _addUpdateText;
        public string AddUpdateText
        {
            get => _addUpdateText;
            set => SetProperty(ref _addUpdateText, value);
        }


        private ProductType _selectType;
        public ProductType SelectType
        {
            get => _selectType;
            set => SetProperty(ref _selectType, value);
        }


        private Categories _selectCategory;
        public Categories SelectCategory
        {
            get => _selectCategory;
            set => SetProperty(ref _selectCategory, value);
        }


        private Shipper _selectShipper;
        public Shipper SelectShipper
        {
            get => _selectShipper;
            set => SetProperty(ref _selectShipper, value);
        }


        public DelegateCommand UnfocusedCommand => new DelegateCommand(Unfocused_Entry);
        public DelegateCommand BackClick => new DelegateCommand(GoBack);
        public DelegateCommand AddProductClick => new DelegateCommand(AddProduct);
        public DelegateCommand DeleteClick => new DelegateCommand(Delete);
        public DelegateCommand AddNewShipper => new DelegateCommand(AddShipper);
        public DelegateCommand<string> AddNewCategoryType => new DelegateCommand<string>(Add_TypeCategory);

        #endregion


        private async void Add_TypeCategory(string model)
        {
            var parameters = new NavigationParameters
                                {
                                  {"entity", model }
                                };
            await _navigationService.NavigateAsync("AddTypeCategory", parameters);
        }

        private async void AddShipper()
        {
            await _navigationService.NavigateAsync("AddShipper");
        }

        private async void AddProduct()
        {

            if (!_isPressed && Product != null && IsValidInput)
            {
                _isPressed = true;
                IsVisibleIndicator = true;

                bool result = await _dataService.InsertUpdateAsync(Product.Id, Product);

                IsVisibleIndicator = false;

                if (result)
                {
                    GoBack();
                }
                else
                {
                    _printMessage.ViewMessage(_localizationManager["ErrorSaveData"]);
                    _isPressed = false;
                }
            }
            else
            {
                _printMessage.ViewMessage(_localizationManager["ErrorSaveData"]);
            }
        }

        private async void Delete()
        {
            if (!_isPressed)
            {
                _isPressed = true;
                bool result = false;
                IsVisibleIndicator = true;

                result = await _dataService.DeleteAsync<Product>(Product.Id);

                IsVisibleIndicator = false;

                if (result)
                {
                    GoBack();
                }
                else
                {
                    _printMessage.ViewMessage(_localizationManager["ErrorDeletData"]);
                    _isPressed = false;
                }
            }
        }

        private async void GoBack()
        {
            await _navigationService.GoBackAsync();
        }

        private void Unfocused_Entry()
        {
            _unfocusedEntry.HideKeyboard();
        }


        #region intarfaces implement

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case "SelectType":
                    if (SelectType != null)
                    {
                        Product.TypeId = SelectType.Id;
                        RaisePropertyChanged("Product");
                    }
                    break;

                case "SelectCategory":
                    if (SelectCategory != null)
                    {
                        Product.CategoryId = SelectCategory.Id;
                        RaisePropertyChanged("Product");
                    }
                    break;

                case "SelectShipper":
                    if (SelectShipper != null)
                    {
                        Product.ShipperId = SelectShipper.Id;
                        RaisePropertyChanged("Product");
                    }
                    break;
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            _isPressed = false;
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            AddUpdateText = _localizationManager["AddProduct"];
            IsDeleteVisible = false;
            _isPressed = false;

            Product = new Product() { };

            ListType = await _dataService.GetDataAsync<ProductType>();
            ListCategory = await _dataService.GetDataAsync<Categories>();
            ListShipper = await _dataService.GetDataAsync<Shipper>();

            if (parameters.ContainsKey("item"))
            {
                Product = parameters.GetValue<Product>("Product");

                AddUpdateText = _localizationManager["UpdateProduct"];
                IsDeleteVisible = true;

                SelectType = ListType.Find(x => x.Id == Product.TypeId);
                SelectCategory = ListCategory.Find(x => x.Id == Product.CategoryId);
                SelectShipper = ListShipper.Find(x => x.Id == Product.ShipperId);
            }

        }

        #endregion
    }
}
