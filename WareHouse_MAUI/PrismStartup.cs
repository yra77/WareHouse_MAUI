

using WareHouse_MAUI.Views;
using WareHouse_MAUI.Models;
using WareHouse_MAUI.ViewModels;
using WareHouse_MAUI.Services.Auth;
using WareHouse_MAUI.Views.Regions;
using WareHouse_MAUI.Services.Interfaces;
using WareHouse_MAUI.Services.Repository;
using WareHouse_MAUI.Services.AccessRoles;
using WareHouse_MAUI.Services.AndroidMedia;
using WareHouse_MAUI.Services.Localization;
using WareHouse_MAUI.Services.DataServices;
using WareHouse_MAUI.Services.VerifyService;
using WareHouse_MAUI.Services.SettingsManager;


namespace WareHouse_MAUI
{
    internal static class PrismStartup
    {
        public static void Configure(PrismAppBuilder builder)
        {
            builder.RegisterTypes(RegisterTypes)
                    .OnAppStart("MainPage/");
        }

        private static void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>()
                             .RegisterForNavigation<Home, HomeViewModel>()
                             .RegisterForNavigation<QrCodeScan, QrCodeScanViewModel>()
                             .RegisterForNavigation<Settings, SettingsViewModel>()
                             .RegisterForNavigation<Products, ProductsViewModel>()
                             .RegisterForNavigation<AddUpdateProduct, AddUpdateProductViewModel>()
                             .RegisterForNavigation<Shippers, ShippersViewModel>()
                             .RegisterForNavigation<AddUpdateShipper, AddUpdateShipperViewModel>()

                         .RegisterInstance(SemanticScreenReader.Default);


            //Regions
            containerRegistry.RegisterForRegionNavigation<QrCodeRegion, QrCodeScanViewModel>();


            //Services
            containerRegistry.RegisterSingleton<IVerifyInputService, VerifyInputService>()
                             .RegisterSingleton<ISettingsManager, SettingsManager>()
                             .RegisterSingleton<IAuth, Auth>()
                             .RegisterSingleton<IRepository, Repository>()
                             .RegisterSingleton<IDataService, DataService>()
                             .RegisterSingleton<IAccessRole, AccessRole>()
                             .RegisterSingleton<ILocalizationManager, LocalizationResourceManager>();


            //Android
#if ANDROID
            containerRegistry.RegisterSingleton<IUnfocusedEntry, Platforms.Android.Services.UnfocusedEntry>()
                             .RegisterSingleton<IChangeThemeService, Platforms.Android.Services.ChangeThemeService>()
                             .RegisterSingleton<ICheck_AndroidServives,
                                                Platforms.Android.Services.Permissions.Android_Services>()
                             .RegisterSingleton<IPrintMessage, Platforms.Android.Services.PrintMessage>()
                             .RegisterSingleton<IAndroidMedia, Platforms.Android.Services.MediaService.AndroidMedia>();

            // containerRegistry.Register<Platforms.Android.Services.Certificate.AndroidMessageHandlerEmitter>();
            //containerRegistry.Register<Platforms.Android.Services.Certificate.TrustProvider>();
            // containerRegistry.RegisterInstance(typeof(Platforms.Android.Services.Certificate.AndroidMessageHandlerEmitter));
            // containerRegistry.RegisterInstance(typeof(Platforms.Android.Services.Certificate.TrustProvider));
#endif
        }
    }
}
