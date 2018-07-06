using Flex.Effects;
using Flex.UWP.Effects;
using System.Linq;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

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

            var image = (Windows.UI.Xaml.Controls.Image)Control;
            //image.Color

            var icon = new BitmapIcon();
            icon.UriSource = ((BitmapImage)image.Source).UriSource;
            icon.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);


            var imageSource = (BitmapImage)image.Source;

            using (var stream = await (RandomAccessStreamReference.CreateFromUri(imageSource.UriSource)).OpenReadAsync())
            {

            }



                var bitmap = new WriteableBitmap(imageSource.PixelWidth, imageSource.PixelHeight);
            for (var x = 0; x < imageSource.PixelWidth; x++)
            {
                for (var y = 0; x < imageSource.PixelHeight; y++)
                {
                    
                }
            }

            //WriteableBitmap bitmap = new WriteableBitmap(100, 100);
            //bitmap.

            //BitmapImage img = ((BitmapImage)image.Source);
            //for (int x = 0; x < img.PixelWidth; x++)
            //{
            //    for (int y = 0; y < img.PixelHeight; y++)
            //    {
            //        Color bitColor = img.GetPixel(x, y);
            //        //Sets all the pixels to white but with the original alpha value
            //        bitmap.SetPixel(x, y, Color.FromArgb(bitColor.A, 255, 255, 255));
            //    }
            //}

        }

        public static Bitmap ChangeColor(Bitmap scrBitmap)
        {
            //You can change your new color here. Red,Green,LawnGreen any..
            Color newColor = Color.Red;
            Color actualColor;
            //make an empty bitmap the same size as scrBitmap
            Bitmap newBitmap = new Bitmap(scrBitmap.Width, scrBitmap.Height);
            for (int i = 0; i < scrBitmap.Width; i++)
            {
                for (int j = 0; j < scrBitmap.Height; j++)
                {
                    //get the pixel from the scrBitmap image
                    actualColor = scrBitmap.GetPixel(i, j);
                    // > 150 because.. Images edges can be of low pixel colr. if we set all pixel color to new then there will be no smoothness left.
                    if (actualColor.A > 150)
                        newBitmap.SetPixel(i, j, newColor);
                    else
                        newBitmap.SetPixel(i, j, actualColor);
                }
            }
            return newBitmap;
        }

        protected override void OnDetached()
        {
            //throw new NotImplementedException();
        }
    }
}
