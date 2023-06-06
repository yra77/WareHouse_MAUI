

using System;
using System.Collections.Generic;


namespace WareHouse_MAUI.Models
{
    internal class TransferOfGoods : BaseModel
    {
        public int? EmployeeId { get; set; } = null;
        public int? WarehouseNumber { get; set; } = 0;
        public int? ClientId { get; set; } = null;

        //code, List count == quantity
        public List<ListCode> Code_Quantity { get; set; } = new List<ListCode>();

        public DateTime DateArrival { get; set; } = DateTime.UtcNow;
    }
}
