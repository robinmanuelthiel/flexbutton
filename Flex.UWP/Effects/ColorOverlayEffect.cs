using Flex.Effects;
using Flex.UWP.Effects;
using System.IO;
using System.Linq;
using System.Numerics;
using Windows.Graphics.Effects;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Composition;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using System;
using System.Runtime.InteropServices.WindowsRuntime;

[assembly: ResolutionGroupName("Flex.Effects")]
[assembly: ExportEffect(typeof(ColorOverlayEffectUWP), nameof(ColorOverlayEffect))]
namespace Flex.UWP.Effects
{
    public class ColorOverlayEffectUWP : PlatformEffect
    {
        protected override void OnAttached()
        {
            var effect = (ColorOverlayEffect)Element.Effects.FirstOrDefault(e => e is ColorOverlayEffect);
            if (effect == null)
                return;

            if (!(Control is Windows.UI.Xaml.Controls.Image))
            {
                return;
            }

            SetOverlay(effect.Color);
        }

        private async void SetOverlay(Color color)
        {
            var formsImage = (Xamarin.Forms.Image)Element;
            if (formsImage?.Source == null)
                return;

            if (formsImage.Width < 0 || formsImage.Height < 0)
                return;

            var nativeColor = Windows.UI.Color.FromArgb((byte)(color.A * 255), (byte)(color.R * 255), (byte)(color.G * 255), (byte)(color.B * 255));

            var uri = new Uri($"ms-appx:///{((FileImageSource)formsImage.Source).File}");        
            var file = await StorageFile.GetFileFromApplicationUriAsync(uri);
            using (IRandomAccessStream ras = await file.OpenAsync(FileAccessMode.Read))
            {
                var decoder = await BitmapDecoder.CreateAsync(ras);
                var provider = await decoder.GetPixelDataAsync(decoder.BitmapPixelFormat, decoder.BitmapAlphaMode, new BitmapTransform(), ExifOrientationMode.RespectExifOrientation, ColorManagementMode.DoNotColorManage);
                byte[] pixels = provider.DetachPixelData();
                for (int i = 0; i < pixels.Length; i += 4)
                {
                    // Writable Bitmap wants colros in BGRA format
                    if (pixels[i + 3] != 0) // Check if color needs to be overwritten when Alpha is set
                    {
                        pixels[i] = nativeColor.B;
                        pixels[i + 1] = nativeColor.G;
                        pixels[i + 2] = nativeColor.R;
                    }                       
                }
                var bitmap = new WriteableBitmap((int)decoder.OrientedPixelWidth, (int)decoder.OrientedPixelHeight);
                using (var stream = bitmap.PixelBuffer.AsStream())
                {
                    await stream.WriteAsync(pixels, 0, pixels.Length);
                }

                if ((Control as Windows.UI.Xaml.Controls.Image) != null)                
                    (Control as Windows.UI.Xaml.Controls.Image).Source = bitmap;                    
            }
        }

        protected override void OnDetached()
        {
            //throw new NotImplementedException();
        }
    }
}
