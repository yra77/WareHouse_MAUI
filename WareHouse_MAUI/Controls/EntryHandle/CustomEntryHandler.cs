

#if ANDROID
using PlatformView = Android.Widget.GridLayout;
#elif (NETSTANDARD || !PLATFORM) || (NET6_0_OR_GREATER && !IOS && !ANDROID)
using PlatformView = System.Object;
#endif
using WareHouse_MAUI.Controls.EntryHandle;
using Microsoft.Maui.Handlers;


namespace WareHouse_MAUI.Controls.EntryHandle
{
    public partial class CustomEntryHandler
    {

        public static PropertyMapper<CustomEntry, CustomEntryHandler> PropertyMapper = new (ViewHandler.ViewMapper)
        {
            [nameof(CustomEntry.LabelName)] = MapLabelName,
            [nameof(CustomEntry.LabelColor)] = MapLabelColor,
            [nameof(CustomEntry.SelectedType)] = MapSelectedType,
            [nameof(CustomEntry.IsValid)] = MapIsValid,
            [nameof(CustomEntry.InputText)] = MapInputText,
            [nameof(CustomEntry.BorderColor)] = MapBorderColor,
        };

        
        public CustomEntryHandler() : base(PropertyMapper)
        {
        }
    }
}
