using Flex.Effects;
using Flex.UWP.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void SetOverlay(Color color)
        {
            var formsImage = (Xamarin.Forms.Image)Element;
            if (formsImage?.Source == null)
                return;

            var image = (Windows.UI.Xaml.Controls.Image)Control;
            //image.Color

            var icon = new BitmapIcon();
            icon.UriSource = ((BitmapImage)image.Source).UriSource;
            icon.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);

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

        protected override void OnDetached()
        {
            //throw new NotImplementedException();
        }
    }
}
