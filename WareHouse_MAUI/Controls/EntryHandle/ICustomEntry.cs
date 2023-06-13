

namespace WareHouse_MAUI.Controls.EntryHandle
{
    public interface ICustomEntry : IView
    {
        string SelectedType { get; set; }
        bool IsValid { get; set; }
        string LabelName { get; set; }
        Color LabelColor { get; set; }
        string InputText { get; set; }
        Color BorderColor { get; set; }
    }
}
