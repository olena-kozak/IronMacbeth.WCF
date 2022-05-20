using System;
using System.IO;
using System.Threading;
using System.Windows.Media.Imaging;

namespace IronMacbeth.Client.Model
{
    public class Image
    {
        private readonly Lazy<BitmapImage> _bitmapImageLazy;

        private readonly MemoryStream _imageData;

        public MemoryStream ImageData 
        {
            get
            {
                _imageData.Seek(0, SeekOrigin.Begin);

                return _imageData;
            }
        }

        public BitmapImage BitmapImage => _bitmapImageLazy.Value;

        public Image(MemoryStream imageData)
        {
            _imageData = imageData;

            _bitmapImageLazy = new Lazy<BitmapImage>(InitializeBitmapImage, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        private BitmapImage InitializeBitmapImage()
        {
            var image = new BitmapImage();

            image.BeginInit();
            image.StreamSource = ImageData;
            image.EndInit();

            return image;
        }
    }
}
