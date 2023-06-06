

namespace WareHouse_MAUI.Controls
{
    internal class MyViewCell : ViewCell
    {
        
        public MyViewCell()
        {
            Tapped += MyViewCell_Tapped;
            
        }


        public static readonly BindableProperty SelectedItemBackgroundColorProperty =
        BindableProperty.Create("SelectedItemBackgroundColor",
                                typeof(Color),
                                typeof(MyViewCell),
                                Color.Parse("#F8F9FA"));

        public Color SelectedItemBackgroundColor
        {
            get { return GetValue(SelectedItemBackgroundColorProperty) as Color; }
            set { SetValue(SelectedItemBackgroundColorProperty, value); }
        }


        private void MyViewCell_Tapped(object sender, EventArgs e)
        {
            this.View.BackgroundColor = SelectedItemBackgroundColor;
        }
    }
}
