


using WareHouse_MAUI.Controls;
using WareHouse_MAUI.Platforms.Android.Services;

using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Compatibility;

using Android.Views.InputMethods;
using android = Android;
using Android.Content;
using Android.Views;


[assembly: ExportRenderer(typeof(MySearch), typeof(My_Search))]
namespace WareHouse_MAUI.Platforms.Android.Services
{
    internal class My_Search : SearchBarRenderer
    {

        public My_Search(Context context) : base(context) {  }

        
        public override bool DispatchTouchEvent(MotionEvent ev)
        {
            if (ev.Action == MotionEventActions.Up)
            {
                InputMethodManager imm = (InputMethodManager)this.Context.GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(this.Control.WindowToken, HideSoftInputFlags.ImplicitOnly);
            }
            return base.DispatchTouchEvent(ev);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {

                ((MySearch)e.NewElement).PropertyChanging += OnPropertyChanging;
            }
            
            if (e.OldElement != null)
            {
                ((MySearch)e.OldElement).PropertyChanging -= OnPropertyChanging;
            }

            var buttonId = Resources.GetIdentifier("android:id/search_button", null, null);
            var butt = Control.FindViewById(buttonId);
            InputMethodManager imm = (InputMethodManager)this.Context.GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(butt.WindowToken, HideSoftInputFlags.None);
            //underline color
            var plateId = Resources.GetIdentifier("android:id/search_plate", null, null);
            var plate = Control.FindViewById(plateId);
            plate.SetBackgroundColor(android.Graphics.Color.Transparent);

            // Disable the Keyboard on Focus
            this.Control.InputType = (int)android.Text.InputTypes.ClassText;
        }

        private void OnPropertyChanging(object sender, PropertyChangingEventArgs propertyChangingEventArgs)
        {
           
            // Check if the view is about to get Focus
            if (propertyChangingEventArgs.PropertyName == VisualElement.IsFocusedProperty.PropertyName)
            {
                // incase if the focus was moved from another Entry
                // Forcefully dismiss the Keyboard 
                InputMethodManager imm = (InputMethodManager)this.Context.GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(this.Control.WindowToken, HideSoftInputFlags.None);
            }
        }

    }
}
