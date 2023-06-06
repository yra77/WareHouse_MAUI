

using WareHouse_MAUI.Controls;
using WareHouse_MAUI.Platforms.Android.Services;

using Android.Content.Res;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Android.Content;
using Android.Views.InputMethods;
using Microsoft.Maui.Controls.Platform;
using Android.Graphics;
using Android.OS;
using color = Android.Graphics.Color;
using Android.Views;


[assembly: ExportRenderer(typeof(MyEntry), typeof(My_Entry))]
namespace WareHouse_MAUI.Platforms.Android.Services
{
    internal class My_Entry : EntryRenderer
    {

        public My_Entry(Context context):base(context) { }


        public override bool DispatchTouchEvent(MotionEvent ev)
        {
            if (ev.Action == MotionEventActions.Up)
            {
                InputMethodManager imm = (InputMethodManager)this.Context.GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(this.Control.WindowToken, HideSoftInputFlags.ImplicitOnly);
            }
            return base.DispatchTouchEvent(ev);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                
                ((MyEntry)e.NewElement).PropertyChanging += OnPropertyChanging;
            }

            if (e.OldElement != null)
            {
                ((MyEntry)e.OldElement).PropertyChanging -= OnPropertyChanging;
            }

            color borderColor = color.Transparent;// ParseColor("#495CDD");

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Control.BackgroundTintList = ColorStateList.ValueOf(borderColor);
            }
            else
            {
                Control.Background.SetColorFilter(borderColor, PorterDuff.Mode.SrcOut);
            }
            // Disable the Keyboard on Focus
            this.Control.ShowSoftInputOnFocus = true;
        }

        private void OnPropertyChanging(object sender, PropertyChangingEventArgs propertyChangingEventArgs)
        {
            // Check if the view is about to get Focus
            if (propertyChangingEventArgs.PropertyName == VisualElement.IsFocusedProperty.PropertyName)
            {
                // incase if the focus was moved from another Entry
                // Forcefully dismiss the Keyboard 
                InputMethodManager imm = (InputMethodManager)this.Context.GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(this.Control.WindowToken, HideSoftInputFlags.ImplicitOnly);
            }
        }

    }
}
