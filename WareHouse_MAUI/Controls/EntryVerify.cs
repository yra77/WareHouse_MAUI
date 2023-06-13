

using System.Text.RegularExpressions;


namespace WareHouse_MAUI.Controls
{
    internal class EntryVerify : Entry
    {

        public EntryVerify()
        {
            TextChanged += DataContextChanged_Async;
            Loaded += EntryVerify_Loaded;
        }


        #region property

        public static readonly BindableProperty SelectedTypeProperty =
        BindableProperty.Create("SelectedType",
                                typeof(string),
                                typeof(EntryVerify));
        public string SelectedType
        {
            get { return GetValue(SelectedTypeProperty) as string; }
            set { SetValue(SelectedTypeProperty, value); }
        }


        public static readonly BindableProperty IsValidProperty =
        BindableProperty.Create("IsValid",
                                typeof(bool),
                                typeof(EntryVerify));
        public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            set { SetValue(IsValidProperty, value); }
        }

        #endregion


        private void EntryVerify_Loaded(object sender, EventArgs e)
        {
            if (Text != null)
            {
                CheckData();
            }
        }

        private bool CheckData()
        {
            var res = false;
            switch (this.SelectedType)
            {
                case "textEN":
                    res = Text_EN_Verify();
                    if (Text.Length > 2)
                    {
                        IsValid = IsValidTextEN();
                    }
                    break;

                case "textUA":
                    res = Text_UA_Verify();
                    if (Text.Length > 2)
                    {
                        IsValid = IsValidTextUA();
                    }
                    break;

                case "email":
                    res = EmailVerify();
                    IsValid = IsValidEmail(Text);
                    break;

                case "digit":
                    res = DigitVerify();
                    if (Text.Length > 0)
                    {
                        IsValid = IsValidDigit();
                    }
                    break;

                case "password":
                    res = PasswordCheckin();
                    IsValid = IsValidPassword(Text);
                    break;

                case "mixed":
                    res = Mixed();
                    if (Text.Length > 0)
                    {
                        IsValid = IsValidMixed();
                    }
                    break;
            }

            return res;
        }

        private async void DataContextChanged_Async(object sender, TextChangedEventArgs e)
        {
            //Debug.WriteLine(Text);

            var res = true;

            if (sender is EntryVerify box)
            {
                if (Text != null && Text.Length > 0)
                {

                    res = CheckData();

                    CursorPosition = Text.Length;

                    if (res || IsValid)
                    {
                        box.TextColor = Colors.Green;
                    }
                    else
                    {
                        box.TextColor = Colors.Red;
                        if (!res)
                            await Task.Delay(1500);
                        box.TextColor = Colors.Green;
                    }
                }
            }
        }

        private bool Text_EN_Verify()
        {
            bool flag = true;
            string temp = Text;

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
                }
            }

            Text = temp;

            return flag;
        }

        private bool IsValidTextEN()
        {
            return Regex.IsMatch(Text, @"^[a-zA-Z]+$");
        }

        private bool Text_UA_Verify()
        {
            bool flag = true;
            string temp = Text;

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
                }
            }

            Text = temp;

            return flag;
        }

        private bool IsValidTextUA()
        {
            return Regex.IsMatch(Text, @"^[А-ЩЮ-Яа-пр-щю-яьЬЇїІіЄє`]+$");
        }

        private bool EmailVerify()
        {
            bool flag = true;
            string temp = Text;

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
                }
            }
            Text = temp;

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
            string temp = Text;

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
                }
            }
            Text = temp;

            return flag;
        }

        private bool IsValidDigit()
        {
            return Regex.IsMatch(Text, @"^[0-9]+$");
        }

        private bool PasswordCheckin()
        {
            bool flag = true;
            string temp = Text;

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
                }
            }

            Text = temp;

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
            string temp = Text;

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
                }
            }

            Text = temp;

            return flag;
        }

        private bool IsValidMixed()
        {
            return Regex.IsMatch(Text, @"^[0-9А-ЩЮ-Яа-пр-щю-яьЬЇїІіЄє`a-zA-Z ]+$");
        }
    }
}
