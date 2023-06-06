

using System.Globalization;


namespace WareHouse_MAUI.Converters
{
    internal class ImageByte : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)

        {
            if (value == null)
            {
                return null;
            }

            var byteArray = value as byte[];

            return ImageSource.FromStream(() => new MemoryStream(byteArray));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value as ImageSource;
        }
    }
}
