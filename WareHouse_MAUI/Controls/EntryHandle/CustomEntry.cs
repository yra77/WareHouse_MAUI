

using Android.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WareHouse_MAUI.Controls.EntryHandle
{
    [ContentProperty(nameof(Content))]
    public class CustomEntry : View, ICustomEntry
    {

        public static readonly BindableProperty ContentProperty = BindableProperty.Create(nameof(Content),
                                            typeof(View), typeof(CustomEntry), default(View));

        public View Content
        {
            get => (View)GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }


        public static readonly BindableProperty SelectedTypeProperty =
       BindableProperty.Create("SelectedType",
                               typeof(string),
                               typeof(CustomEntry));
        public string SelectedType
        {
            get { return GetValue(SelectedTypeProperty) as string; }
            set { SetValue(SelectedTypeProperty, value); }
        }


        public static readonly BindableProperty IsValidProperty =
        BindableProperty.Create("IsValid",
                                typeof(bool),
                                typeof(CustomEntry),
                               defaultBindingMode:BindingMode.TwoWay);
        public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            set { SetValue(IsValidProperty, value); }
        }


        public static readonly BindableProperty LabelNameProperty =
            BindableProperty.Create(nameof(LabelName), typeof(string), typeof(CustomEntry), null);
        public string LabelName
        {
            get => (string)GetValue(LabelNameProperty);
            set => SetValue(LabelNameProperty, value);
        }


        public static readonly BindableProperty LabelColorProperty =
            BindableProperty.Create(nameof(LabelColor), typeof(Color), typeof(CustomEntry), null);
        public Color LabelColor
        {
            get => (Color)GetValue(LabelColorProperty);
            set => SetValue(LabelColorProperty, value);
        }


        public static readonly BindableProperty InputTextProperty =
                               BindableProperty.Create(nameof(InputText), typeof(string),
                                   typeof(CustomEntry), null,
                               defaultBindingMode: BindingMode.TwoWay);
        public string InputText
        {
            get => (string)GetValue(InputTextProperty);
            set => SetValue(InputTextProperty, value);
        }


        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CustomEntry), null);
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }
    }
}
