

using System.Reflection;


namespace WareHouse_MAUI.Converters
{
    internal class ImageToByteArray
    {

        public static byte[] ToByteArr(string name)
        {
            byte[] buffer = null;
            try
            {
                Assembly assembly = typeof(ImageToByteArray).GetTypeInfo().Assembly;

                Stream stream = assembly.GetManifestResourceStream("WareHouse_MAUI.Resources.Images." + name);
                long length = stream.Length;
                buffer = new byte[length];
                stream.Read(buffer, 0, (int)length);
                stream.Close();
            }
            catch { }

            return buffer;
        }
    }
}
