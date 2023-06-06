

namespace WareHouse_MAUI.Models
{
    internal class ListCode
    {
        public int Id { get; set; }

        public string Code { get; set; } = "000000000000";
        public int? Quantity { get; set; } = 0;

        public int? ConsignmentId { get; set; } = null;
        public int? ConsignmentId2 { get; set; } = null;
    }
}
