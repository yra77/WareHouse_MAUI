

namespace WareHouse_MAUI.Controls
{
    internal class ButtonForColor : Button
    {

        private static List<ButtonForColor> instance;
        private bool _isPressed = false;


        static ButtonForColor()
        {
            instance = new List<ButtonForColor>();
        }

        public ButtonForColor() : base()
        {
            instance.Add(this);
            Pressed += MyButton_Pressed;
        }


        private void MyButton_Pressed(object sender, EventArgs e)
        {
            if(Is_Pressed() == false && _isPressed == false)
            {
                CornerRadius = 5;
                _isPressed = true;
            }
            else
            {
                CornerRadius = 25;
                _isPressed = false;
            }
        }

        private bool Is_Pressed()
        {
            foreach (var item in instance)
            {
                if (item._isPressed == true)
                {
                    item.CornerRadius = 25;
                    item._isPressed = false;
                }
            }
            return false;
        }

    }
}
