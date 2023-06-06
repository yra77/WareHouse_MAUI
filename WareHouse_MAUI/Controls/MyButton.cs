

namespace WareHouse_MAUI.Controls
{
    internal class MyButton : Button
    {

        private bool _isPressed = false;

        public MyButton() : base()
        {
            this.BackgroundColor = Color.Parse("#4297F4");
            Pressed += MyButton_Pressed; 
        }


        #region property

        public static readonly BindableProperty PressedStateProperty = BindableProperty.Create(nameof(PressedState),
                            returnType: typeof(bool),
                            declaringType: typeof(MyButton),
                            defaultBindingMode: BindingMode.TwoWay,
                            propertyChanged: PressedStateChanged);

        public bool PressedState
        {
            get { return (bool)GetValue(PressedStateProperty); }
            set { SetValue(PressedStateProperty, value); }
        }

        #endregion

        //якщо змінюється преседстейт у вьюмодел, то змінюємо колір кнопки
        private static void PressedStateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is MyButton myBtn && newValue is bool isValue)
            {
                if(isValue)
                {
                    myBtn.BackgroundColor = Color.Parse("#B0FF93");
                    myBtn._isPressed = true;
                }
                else
                {
                    myBtn.BackgroundColor = Color.Parse("#4297F4");
                    myBtn._isPressed = false;
                }
            }
        }

        private void MyButton_Pressed(object sender, EventArgs e)
        {
            if (!_isPressed)
            {
                this.BackgroundColor = Color.Parse("#B0FF93");
                _isPressed = true;
                PressedState = true;
            }
            else
            {
                this.BackgroundColor = Color.Parse("#4297F4");
                _isPressed = false;
                PressedState = false;
            }
        }
    }
}
