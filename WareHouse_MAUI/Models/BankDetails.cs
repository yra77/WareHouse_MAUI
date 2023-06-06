

namespace WareHouse_MAUI.Models
{
    public class BankDetails : BaseModel
    {
        public string BankAccounts { get; set; } = "0000000000000000";

        public int? EmployeeId { get; set; } = null;
        public int? ShipperId { get; set; } = null;
        public int? ClientId { get; set; } = null;
    }
}
