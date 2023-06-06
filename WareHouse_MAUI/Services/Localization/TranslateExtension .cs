

namespace WareHouse_MAUI.Services.Localization
{
    [ContentProperty(nameof(Name))]
    internal class TranslateExtension : IMarkupExtension<BindingBase>
    {
        public string? Name { get; set; }

        public BindingBase ProvideValue(IServiceProvider serviceProvider)
        {
            return new Binding
            {
                Mode = BindingMode.OneWay,
                Path = $"[{Name}]",
                Source = LocalizationResourceManager.Instance
            };
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) => ProvideValue(serviceProvider);
    }
}
