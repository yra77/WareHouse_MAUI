

namespace WareHouse_MAUI.Services.VerifyService
{
    public interface IVerifyInputService
    {
        bool IsValidEmail(string email);
        bool EmailVerify(ref string str);
        bool TelVerify(string number);
        bool NameVerify(ref string str);
        bool PasswordCheckin(ref string str);
        bool PasswordVerify(string str);
        bool PositionVerify(ref string str);
        string NumCardVerify(string num);
        string NumVerify(string temp, bool isDate);
    }
}
