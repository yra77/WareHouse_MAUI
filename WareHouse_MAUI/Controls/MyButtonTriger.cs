

namespace WareHouse_MAUI.Controls
{
    internal class MyButtonTriger : Button
    {

        private static List<MyButtonTriger> instance;
        private bool _isPressed = false;


        static MyButtonTriger()
        {
            instance = new List<MyButtonTriger>();
        }

        public MyButtonTriger() : base()
        {
            instance.Add(this);
            Pressed += MyButton_Pressed;
        }


        private void MyButton_Pressed(object sender, EventArgs e)
        {
            if (Is_Pressed() == false && _isPressed == false)
            {
                BackgroundColor = Color.Parse("#4297F4");
                _isPressed = true;
            }
            else
            {
                BackgroundColor = Color.Parse("#C8C8C8");
                _isPressed = false;
            }
        }

        private bool Is_Pressed()
        {
            foreach (var item in instance)
            {
                if (item._isPressed == true)
                {
                    item.BackgroundColor = Color.Parse("#C8C8C8");
                    item._isPressed = false;
                }
            }
            return false;
        }

    }
}
