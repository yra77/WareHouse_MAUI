

using WareHouse_MAUI.Services.AndroidMedia;
using Android.Media;


namespace WareHouse_MAUI.Platforms.Android.Services.MediaService
{
    internal class AndroidMedia : IAndroidMedia
    {

        private MediaPlayer _mediaPlayer;


        public AndroidMedia()
        {
            _mediaPlayer = new MediaPlayer();
        }

        public void GetBeep()
        {
            var context = MainActivity.Inst;

            if (_mediaPlayer == null)
            {
                _mediaPlayer = new MediaPlayer();
            }
            else
            {
                var descriptor = context.Assets.OpenFd("beep.mp3");

                _mediaPlayer.Reset();
                _mediaPlayer.SetDataSource(descriptor.FileDescriptor, descriptor.StartOffset, descriptor.Length);
                _mediaPlayer.Prepare();
                _mediaPlayer.Start();
            }
        }
    }
}
