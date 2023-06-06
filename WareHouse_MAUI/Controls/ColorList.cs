

using Microsoft.Maui.Controls.Shapes;


namespace WareHouse_MAUI.Controls
{
    internal class ColorList : StackLayout
    {

        private bool _isChange = false;

        public ColorList()
        {
            PropertyChanged += ColorList_PropertyChanged1;
        }

        private void ColorList_PropertyChanged1(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!_isChange && Items != null)
            {

                _isChange = true;

                _ = Task.Run(() =>
                {

                    Orientation = StackOrientation.Horizontal;
                    Spacing = 7;

                    for(int i = 0; i < Items.Count; i++)
                    {
                        Ellipse ellipse = new Ellipse()
                        {
                            Stroke = Color.Parse("LightGray"),
                            StrokeThickness = 1,
                            Fill = Items[i],
                            WidthRequest = 12,
                            HeightRequest = 12
                        };

                        Children.Add(ellipse);
                    }
                });
            }
        }

        public static readonly BindableProperty ItemsProperty =
          BindableProperty.Create(
             propertyName: nameof(Items),
              returnType: typeof(List<Color>),
              declaringType: typeof(ColorList),
              defaultValue: null,
              defaultBindingMode: BindingMode.TwoWay);

        public List<Color> Items
        {
            get
            {
                return (List<Color>)GetValue(ItemsProperty);
            }
            set
            {
                SetValue(ItemsProperty, value);
            }
        }


    }
}
