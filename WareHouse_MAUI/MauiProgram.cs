﻿

using WareHouse_MAUI.Controls;
using BarcodeScanner.Mobile;
using Microsoft.Maui.Controls.Compatibility.Hosting;


namespace WareHouse_MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UsePrismApp<App>(PrismStartup.Configure)
                .ConfigureMauiHandlers((handlers) =>
                {
#if ANDROID
                    handlers.AddCompatibilityRenderer(typeof(MyEntry), typeof(WareHouse_MAUI.Platforms.Android.Services.My_Entry));
#endif
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })

                //barcode scan conf
                .ConfigureMauiHandlers(handlers =>
                {
                    // Add the handlers
                    handlers.AddBarcodeScannerHandler();
                });

#if ANDROID
            Platforms.Android.Services.Certificate.DangerousAndroidMessageHandlerEmitter.Register();
            Platforms.Android.Services.Certificate.DangerousTrustProvider.Register();
#endif

            builder.Services.AddLocalization();
            builder.UseMauiCompatibility();
            builder.ConfigureAnimations();
           
            return builder.Build();
        }
    }
}
