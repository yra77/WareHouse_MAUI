

using WareHouse_MAUI.Services.Localization;
using WareHouse_MAUI.Services.SettingsManager;
using System.ComponentModel;
using System.Globalization;


namespace WareHouse_MAUI.ViewModels
{
    internal class SettingsViewModel : BaseViewModel, INavigationAware
    {


        public SettingsViewModel(INavigationService navigationService,
                                ISettingsManager settingsManager,
                                ILocalizationManager localizationManager)
        {
            _navigationService = navigationService;
            _settingsManager = settingsManager;
            _localizationManager = localizationManager;
        }


        #region property

        private bool _isUkrainian;
        public bool IsUkrainian
        {
            get => _isUkrainian;
            set => SetProperty(ref _isUkrainian, value);
        }

        private bool _isEnglish;
        public bool IsEnglish
        {
            get => _isEnglish;
            set => SetProperty(ref _isEnglish, value);
        }

        public DelegateCommand BackBtn => new DelegateCommand(GoBack);

        #endregion


        private async void GoBack()
        {
           await _navigationService.GoBackAsync();
        }


        #region implement interface

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);


            switch (args.PropertyName)
            {
                case "IsEnglish":
                    IsUkrainian = false;
                    _localizationManager.SetCulture(CultureInfo.CreateSpecificCulture("en"));
                    _settingsManager.Language = "en";
                    break;
                case "IsUkrainian":
                    IsEnglish = false;
                    _localizationManager.SetCulture(CultureInfo.CreateSpecificCulture("uk"));
                    _settingsManager.Language = "uk";
                    break;
                default:
                    break;
            }


        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (_settingsManager.Language == null || _settingsManager.Language == "uk")
            {
                IsUkrainian = true;
                IsEnglish = false;
            }
            else
            {
                IsEnglish = true;
                IsUkrainian = false;
            }
        }

        #endregion
    }
}
