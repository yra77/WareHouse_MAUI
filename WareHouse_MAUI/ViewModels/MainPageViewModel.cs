

using WareHouse_MAUI.Models;
using WareHouse_MAUI.Services.Auth;
using WareHouse_MAUI.Services.Interfaces;
using WareHouse_MAUI.Services.AccessRoles;
using WareHouse_MAUI.Services.Localization;
using WareHouse_MAUI.Services.VerifyService;
using WareHouse_MAUI.Services.SettingsManager;

using System.ComponentModel;
using System.Globalization;


namespace WareHouse_MAUI.ViewModels
{
    internal class MainPageViewModel : BaseViewModel, INavigatedAware
    {


        public MainPageViewModel(ILocalizationManager localizationManager,
                             INavigationService navigationService,
                             ICheck_AndroidServives checkAndroid,
                             ISettingsManager settingsManager,
                             IVerifyInputService verifyInput,
                             IChangeThemeService changeTheme,
                             IUnfocusedEntry unfocusedEntry,
                             IPrintMessage printMessage,
                             IAccessRole accessRole,
                             IAuth auth)
        {
            _localizationManager = localizationManager;
            _navigationService = navigationService;
            _settingsManager = settingsManager;
            _unfocusedEntry = unfocusedEntry;
            _checkAndroid = checkAndroid;
            _printMessage = printMessage;
            _changeTheme = changeTheme;
            _verifyInput = verifyInput;
            _accessRole = accessRole;
            _auth = auth;

            Email = "";
            Password = "";
            IsEnabled = false;

            ImagePassword = "ic_eye_off.png";
            IsVisiblePassword = true;

            Color_OkBtn = "LightGrey";
            EmailBorderColor = Color.Parse("White");
            PassBorderColor = Color.Parse("White");


            if (_settingsManager.Language == null)
            {
                _settingsManager.Language = "uk";
                _localizationManager.SetCulture(CultureInfo.CreateSpecificCulture("uk"));
            }
            else
            {
                if (_settingsManager.Language == "uk")
                {
                    _localizationManager.SetCulture(CultureInfo.CreateSpecificCulture("uk"));
                }
                else
                {
                    _localizationManager.SetCulture(CultureInfo.CreateSpecificCulture("en"));
                }
            }

            PassText = _localizationManager["Email"];
            EmailText = _localizationManager["Password"];
            HighText = _localizationManager["HomeText"];
        }


        #region Public property

        private string _emailText;
        public string EmailText
        {
            get => _emailText;
            set => SetProperty(ref _emailText, value);
        }


        private string _passText;
        public string PassText
        {
            get => _passText;
            set => SetProperty(ref _passText, value);
        }


        private string _highText;
        public string HighText
        {
            get => _highText;
            set => SetProperty(ref _highText, value);
        }


        private string _errorEmailText;
        public string ErrorEmailText
        {
            get => _errorEmailText;
            set => SetProperty(ref _errorEmailText, value);
        }


        private string _errorPassText;
        public string ErrorPassText
        {
            get => _errorPassText;
            set => SetProperty(ref _errorPassText, value);
        }


        private Color _emailBorderColor;
        public Color EmailBorderColor
        {
            get => _emailBorderColor;
            set => SetProperty(ref _emailBorderColor, value);
        }


        private Color _passBorderColor;
        public Color PassBorderColor
        {
            get => _passBorderColor;
            set => SetProperty(ref _passBorderColor, value);
        }


        private string _color_OkBtn;
        public string Color_OkBtn
        {
            get => _color_OkBtn;
            set => SetProperty(ref _color_OkBtn, value);
        }


        private string _imagePassword;
        public string ImagePassword
        {
            get => _imagePassword;
            set => SetProperty(ref _imagePassword, value);
        }


        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }


        private bool _isVisiblePassword;
        public bool IsVisiblePassword
        {
            get => _isVisiblePassword;
            set => SetProperty(ref _isVisiblePassword, value);
        }


        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }


        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }


        public DelegateCommand SignInCommand =>
            new DelegateCommand(SignInAsync, IsSignInEnable).ObservesProperty(() => IsEnabled);
        public DelegateCommand Btn_IsVisiblePassword => new DelegateCommand(Click_IsVisiblePassword);
        public DelegateCommand UnfocusedCommand => new DelegateCommand(Unfocused_Entry);

        #endregion


        private async void SignInAsync()
        {
            _isPressed = true;
            IsSignInEnable();
            IsVisibleIndicator = true;

            var result = await _auth.AuthAsync(Password, Email);
            Unfocused_Entry();

            IsVisibleIndicator = false;

            if (result.Item1)
            {
                _settingsManager.Email = Email;
                _settingsManager.Password = Password;

                _employee = result.Item2.Item2;
                _accessRole.SetRole(_employee.Role);

                await _navigationService.NavigateAsync("Home");
            }
            else
            {
                _printMessage.ViewMessage(result.Item2.Item1);
                _isPressed = false;
            }
        }

        private void CheckEmail()
        {

            if (_email.Length > 0)
            {
                EmailBorderColor = Color.Parse("Red");
                ErrorEmailText = _localizationManager["ErrorText_Email"];// Resources.Strings.Resource.ErrorText_Email; ;

                string email = Email;

                if (!_verifyInput.EmailVerify(ref email))
                {
                    Email = email;
                }
                else
                {
                    if (_verifyInput.IsValidEmail(Email))
                    {
                        ErrorEmailText = "Ok!";
                        EmailBorderColor = Color.Parse("White");
                    }
                }

                IsSignInEnable();
            }
        }

        private void CheckPassword()
        {

            if (_password.Length > 0)
            {
                PassBorderColor = Color.Parse("Red");
                ErrorPassText = _localizationManager["ErrorText_Password"];// Resources.Strings.Resource.ErrorText_Password;

                string password = Password;

                if (!_verifyInput.PasswordCheckin(ref password))
                {
                    Password = password;
                }
                else
                {
                    if (_verifyInput.PasswordVerify(Password) && (Password.Length > 7 && Password.Length < 17))
                    {
                        ErrorPassText = "Ok!";
                        PassBorderColor = Color.Parse("White");
                    }
                }

                IsSignInEnable();
            }
        }

        private void Click_IsVisiblePassword()//password visible or hidden
        {
            if (IsVisiblePassword)
            {
                IsVisiblePassword = false;
                ImagePassword = "ic_eye.png";
            }
            else
            {
                IsVisiblePassword = true;
                ImagePassword = "ic_eye_off.png";
            }
        }

        private bool IsSignInEnable()//Enable disable "Sign in" Button
        {
            IsEnabled = (!_isPressed && _stateNethwork
                && PassBorderColor == Color.Parse("White")
                && EmailBorderColor == Color.Parse("White")
                    && _email.Length > 0
                    && _password.Length > 0)
                ? IsEnabled = true : IsEnabled = false;

            if (IsEnabled)
            {
                Color_OkBtn = "#4285F4";
            }
            else
            {
                Color_OkBtn = "LightGrey";
            }

            return IsEnabled;
        }

        private void Unfocused_Entry()
        {
            _unfocusedEntry.HideKeyboard();
        }


        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case "Email":
                    CheckEmail();
                    break;
                case "Password":
                    CheckPassword();
                    break;
                default:
                    break;
            }
        }

        protected override void IsNethwork(bool state)
        {
            _stateNethwork = state;

            if (_stateNethwork)
            {
                _ = Task.Run( () =>
                {
                    if (_staticList == null || _staticList.Count == 0)
                    {
                        //var temp = await _products.GetProductsAsync();
                        //_staticList = new ObservableCollection<ProductWithList>(ToProductWithLists.ConvertToProductWithLists(temp));
                    }
                });
            }

            IsSignInEnable();
        }



        #region implement intarface

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            _isPressed = false;
            IsSignInEnable();
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            await Task.Delay(250);

            await _checkAndroid.CheckServices(_changeNethwork);

            Email = "";
            Password = "";
            IsEnabled = false;

            ImagePassword = "ic_eye_off.png";
            IsVisiblePassword = true;

            Color_OkBtn = "LightGrey";
            EmailBorderColor = Color.Parse("White");
            PassBorderColor = Color.Parse("White");

            IsSignInEnable();

            if (_stateNethwork)
            {
                _ = Task.Run( () =>
                {
                    if (_staticList == null || _staticList.Count == 0)
                    {
                        //var temp = await _products.GetProductsAsync();
                        //_staticList = new ObservableCollection<ProductWithList>(ToProductWithLists.ConvertToProductWithLists(temp));
                    }
                });
            }

#if DEBUG
            if (_settingsManager.Email != null
                && _settingsManager.Password != null)
            {
                Email = _settingsManager.Email;
                Password = _settingsManager.Password;
            }
#endif
        }

        #endregion
    }
}
