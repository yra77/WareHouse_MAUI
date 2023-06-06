

using System.Collections.Generic;


namespace WareHouse_MAUI.Models
{
    internal class Shipper : BaseModel
    {
        public string Phone { get; set; }
        public List<BankDetails> BankAccount { get; set; }
    }
}
