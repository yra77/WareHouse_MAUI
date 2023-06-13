

using Android.Graphics.Drawables;
using color = Android.Graphics.Color;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Microsoft.Maui.Controls.Platform;
using Android.Widget;
using Android.Content.Res;
using Google.Android.Material.TextField;
using Orientation = Android.Widget.GridOrientation;
using Android.Views.InputMethods;
using System.Text.RegularExpressions;


namespace WareHouse_MAUI.Controls.EntryHandle
{
    public partial class CustomEntryHandler : ViewHandler<CustomEntry, GridLayout>
    {

        private CustomEntry _entryBase;
        private TextView _textview;
        private GridLayout _layout;
        private GradientDrawable _shape;
        private TextInputEditText _textedit;

        private string _selectedType;
        private bool _isValid;
        private Color _borderColor;


        protected override GridLayout CreatePlatformView()
        {

            color borderColor = color.Transparent;// ParseColor("#495CDD");

            _layout = new GridLayout(Context);
            _layout.HorizontalFadingEdgeEnabled = false;
            _layout.BackgroundTintList = ColorStateList.ValueOf(borderColor);
            _layout.RowCount = 2;
            _layout.Orientation = Orientation.Vertical;

            _textedit = new TextInputEditText(_layout.Context);
            _textedit.SetMinimumWidth(1250);
            _textedit.TextSize = 14;
            _textedit.SetLengthFilter(50);
            _textedit.ClipToOutline = false;
            _textedit.SetSingleLine();
            _textedit.ImeOptions = ImeAction.Done;
            _textedit.TextChanged += Textedit_TextChanged;
            _textedit.FocusChange += Textedit_FocusChange;

            _shape = new GradientDrawable();
            _shape.SetShape(ShapeType.Rectangle);
            _shape.SetCornerRadius(0);
            _shape.SetStroke(3, color.Gray);
            _textedit.SetBackground(_shape);
            _textedit.SetPadding(20, 25, 20, 25);


            _textview = new TextView(_layout.Context)
            {
                TextSize = 16,
                Typeface = Android.Graphics.Typeface.Default,
                TranslationY = 85,
                TranslationX = 15
            };

            _layout.AddView(_textview);
            _layout.AddView(_textedit);

            return _layout;
        }

        protected override void ConnectHandler(GridLayout platformView)
        {
            base.ConnectHandler(platformView);
        }

        protected override void DisconnectHandler(GridLayout platformView)
        {
            // Perform any native view cleanup here
            platformView.Dispose();
            base.DisconnectHandler(platformView);
        }

        public static void MapLabelName(CustomEntryHandler handler, CustomEntry view)
        {
            handler._textview.Text = view.LabelName;
        }

        public static void MapLabelColor(CustomEntryHandler handler, CustomEntry view)
        {
            handler._textview.SetTextColor(view.LabelColor.ToPlatform());
        }

        public static void MapInputText(CustomEntryHandler handler, CustomEntry view)
        {
            if (view.InputText != null && handler._textedit.Text != null)
            {
                handler._textedit.Text = view.InputText;
            }
        }

        public static void MapBorderColor(CustomEntryHandler handler, CustomEntry view)
        {
            handler._borderColor = view.BorderColor;
            handler._shape.SetStroke(3, view.BorderColor.ToPlatform());
        }

        public static void MapSelectedType(CustomEntryHandler handler, CustomEntry view)
        {
            handler._selectedType = view.SelectedType;
        }

        public static void MapIsValid(CustomEntryHandler handler, CustomEntry view)
        {
            handler._entryBase = view;
            handler._isValid = view.IsValid;
        }

        private void Textedit_FocusChange(object sender, Android.Views.View.FocusChangeEventArgs e)
        {
            var name = sender as TextInputEditText;
            InputMethodManager imm = (InputMethodManager)this.Context.GetSystemService(Android.Content.Context.InputMethodService);

            var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            Android.OS.IBinder wToken = activity.CurrentFocus?.WindowToken;
            imm.HideSoftInputFromWindow(wToken, HideSoftInputFlags.ImplicitOnly);
        }

        private void Textedit_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (e.Text == null || e.Text.ToString() == "")
            {
                _textview.TranslationY = 85;
                _textview.TranslationX = 15;
                _textview.ScaleX = 1f;
                _textview.ScaleY = 1f;
            }
            else
            {
                _textview.TranslationY = 0;
                _textview.TranslationX = 0;
                _textview.ScaleX = 0.9f;
                _textview.ScaleY = 0.9f;
            }

            if (e.Text != null && e.Text.ToString().Length > 0)
            {
                TextChanged();
                CheckData();
                _entryBase.IsValid = _isValid;
            }
            else
            {
                _shape.SetStroke(3, _borderColor.ToPlatform());
            }
        }

        private bool CheckData()
        {
            var res = false;
            switch (_selectedType)
            {
                case "textEN":
                    res = Text_EN_Verify();
                    if (_textedit.Text.Length > 2)
                    {
                        _isValid = IsValidTextEN();
                    }
                    break;

                case "textUA":
                    res = Text_UA_Verify();
                    if (_textedit.Text.Length > 2)
                    {
                        _isValid = IsValidTextUA();
                    }
                    break;

                case "email":
                    res = EmailVerify();
                    _isValid = IsValidEmail(_textedit.Text);
                    break;

                case "digit":
                    res = DigitVerify();
                    if (_textedit.Text.Length > 0)
                    {
                        _isValid = IsValidDigit();
                    }
                    break;

                case "password":
                    res = PasswordCheckin();
                    _isValid = IsValidPassword(_textedit.Text);
                    break;

                case "mixed":
                    res = Mixed();
                    if (_textedit.Text.Length > 0)
                    {
                        _isValid = IsValidMixed();
                    }
                    break;
            }

            return res;
        }

        private void TextChanged()
        {

            var res = true;

            if (_textedit.Text != null && _textedit.Text.Length > 0)
            {
                res = CheckData();

                _textedit.SetSelection(_textedit.Text.Length);

                if (res || _isValid)
                {
                    _shape.SetStroke(3, color.DarkGreen);
                }
                if (!res || !_isValid)
                {
                    _shape.SetStroke(3, color.Red);
                }
            }
        }

        private bool Text_EN_Verify()
        {
            bool flag = true;
            string temp = _textedit.Text;

            for (int i = 0; i < temp.Length; i++)
            {

                if ((temp[i] >= 'A' && temp[i] <= 'Z') || (temp[i] >= 'a' && temp[i] <= 'z'))
                {
                    continue;
                }
                else
                {
                    temp = temp.Remove(i, 1);
                    flag = false;
                    _textedit.Text = temp;
                }
            }

            return flag;
        }

        private bool IsValidTextEN()
        {
            return Regex.IsMatch(_textedit.Text, @"^[a-zA-Z]+$");
        }

        private bool Text_UA_Verify()
        {
            bool flag = true;
            string temp = _textedit.Text;

            for (int i = 0; i < temp.Length; i++)
            {

                if ((temp[i] >= 'А' && temp[i] <= 'Щ')
                    || (temp[i] >= 'Ю' && temp[i] <= 'Я') || (temp[i] >= 'а' && temp[i] <= 'п') || (temp[i] >= 'р' && temp[i] <= 'щ')
                    || (temp[i] >= 'ю' && temp[i] <= 'я') || temp[i] == 'Ь' || temp[i] == 'ь' || temp[i] == 'Ї' || temp[i] == 'ї'
                    || temp[i] == 'І' || temp[i] == 'і' || temp[i] == 'Є' || temp[i] == 'є' || temp[i] == '`' || temp[i] == 'ь')
                {
                    continue;
                }
                else
                {
                    temp = temp.Remove(i, 1);
                    flag = false;
                    _textedit.Text = temp;
                }
            }

            return flag;
        }

        private bool IsValidTextUA()
        {
            return Regex.IsMatch(_textedit.Text, @"^[А-ЩЮ-Яа-пр-щю-яьЬЇїІіЄє`]+$");
        }

        private bool EmailVerify()
        {
            bool flag = true;
            string temp = _textedit.Text;

            for (int i = 0; i < temp.Length; i++)
            {

                if (char.IsDigit(temp[i]) || (temp[i] >= 'A' && temp[i] <= 'Z') || (temp[i] >= 'a' && temp[i] <= 'z')
                    || temp[i] == '.' || temp[i] == '@' || temp[i] == '_' || temp[i] == '-')
                {
                    continue;
                }
                else
                {
                    temp = temp.Remove(i, 1);
                    flag = false;
                    _textedit.Text = temp;
                }
            }

            return flag;
        }

        public bool IsValidEmail(string email)
        {
            Regex regex =
         new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
         RegexOptions.CultureInvariant | RegexOptions.Singleline);

            return regex.IsMatch(email);
        }

        private bool DigitVerify()
        {
            bool flag = true;
            string temp = _textedit.Text;

            for (int i = 0; i < temp.Length; i++)
            {

                if (char.IsDigit(temp[i]))
                {
                    continue;
                }
                else
                {
                    temp = temp.Remove(i, 1);
                    flag = false;
                    _textedit.Text = temp;
                }
            }

            return flag;
        }

        private bool IsValidDigit()
        {
            return Regex.IsMatch(_textedit.Text, @"^[0-9]+$");
        }

        private bool PasswordCheckin()
        {
            bool flag = true;
            string temp = _textedit.Text;

            for (int i = 0; i < temp.Length; i++)
            {

                if (char.IsDigit(temp[i]) || (temp[i] >= 'A' && temp[i] <= 'Z') || (temp[i] >= 'a' && temp[i] <= 'z'))
                {
                    continue;
                }
                else
                {
                    temp = temp.Remove(i, 1);
                    flag = false;
                    _textedit.Text = temp;
                }
            }

            return flag;
        }

        private bool IsValidPassword(string str)
        {

            Regex regex = new Regex(@"^([A-Z]{1}[A-Za-z0-9]{0,}[0-9]{1,})",
             RegexOptions.CultureInvariant | RegexOptions.Singleline);

            return regex.IsMatch(str);
        }

        private bool Mixed()
        {
            bool flag = true;
            string temp = _textedit.Text;

            for (int i = 0; i < temp.Length; i++)
            {

                if (char.IsDigit(temp[i]) || (temp[i] >= 'A' && temp[i] <= 'Z') || (temp[i] >= 'a' && temp[i] <= 'z')
                    || (temp[i] >= 'А' && temp[i] <= 'Щ')
                    || (temp[i] >= 'Ю' && temp[i] <= 'Я') || (temp[i] >= 'а' && temp[i] <= 'п') || (temp[i] >= 'р' && temp[i] <= 'щ')
                    || (temp[i] >= 'ю' && temp[i] <= 'я') || temp[i] == 'Ь' || temp[i] == 'ь' || temp[i] == 'Ї' || temp[i] == 'ї'
                    || temp[i] == 'І' || temp[i] == 'і' || temp[i] == 'Є' || temp[i] == 'є' || temp[i] == '`' || temp[i] == ' ')
                {
                    continue;
                }
                else
                {
                    temp = temp.Remove(i, 1);
                    flag = false;
                    _textedit.Text = temp;
                }
            }

            return flag;
        }

        private bool IsValidMixed()
        {
            return Regex.IsMatch(_textedit.Text, @"^[0-9А-ЩЮ-Яа-пр-щю-яьЬЇїІіЄє`a-zA-Z ]+$");
        }

    }
}
