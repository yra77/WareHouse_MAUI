

namespace WareHouse_MAUI.Models
{
    public class Employee : HumanBaseModel
    {
        public string Password { get; set; }
        public int Age { get; set; }
        public byte[] Photo { get; set; } = new byte[0];
        public string JobTitle { get; set; }
        public string Role { get; set; }
        public List<BankDetails> BankAccount { get; set; } = new List<BankDetails>(3);
    }
}
