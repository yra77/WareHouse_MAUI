

using WareHouse_MAUI.Controls;

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
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(EntryVerify), typeof(WareHouse_MAUI.Platforms.Android.Services.EntryVerify))]
namespace WareHouse_MAUI.Platforms.Android.Services
{
    internal class EntryVerify : EntryRenderer
    {

        public EntryVerify(Context context) : base(context)
        {
          
        }

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
                ((Controls.EntryVerify)e.NewElement).PropertyChanging += OnPropertyChanging;
            }

            if (e.OldElement != null)
            {
                ((Controls.EntryVerify)e.OldElement).PropertyChanging -= OnPropertyChanging;
            }

          //  color borderColor = color.Transparent;// ParseColor("#495CDD");

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
              //  Control.BackgroundTintList = ColorStateList.ValueOf(borderColor);  //underline
                UpdateBorders();
            }
            else
            {
                UpdateBorders();
              //  Control.Background.SetColorFilter(borderColor, PorterDuff.Mode.SrcOut);
            }
            // Disable the Keyboard on Focus
            this.Control.ShowSoftInputOnFocus = true;
        }

        void UpdateBorders()
        {//create border
            GradientDrawable shape = new GradientDrawable();
            shape.SetShape(ShapeType.Rectangle);
            shape.SetCornerRadius(0);
            shape.SetStroke(3, color.Gray);
            this.Control.SetBackground(shape);
            this.Control.SetPadding(20,25,20,25);//padding
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
