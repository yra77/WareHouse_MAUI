

using System;


namespace WareHouse_MAUI.Models
{
    internal class Product : BaseModel
    {
        public int? TypeId { get; set; } = 0;
        public int? CategoryId { get; set; } = 0;
        public int? Quantity { get; set; } = 0;
        public int? Price { get; set; } = 0;
        public int? ShipperId { get; set; } = 0;
        public string Code { get; set; } //может быть проблемма с отсутствием ? 
        public int WarehouseNumber { get; set; }
        public DateTime DateArrival { get; set; } = DateTime.UtcNow;
    }
}
