using System;
using System.Drawing;
using System.Linq;
using CoreGraphics;
using Flex.Effects;
using Flex.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("Flex.Effects")]
[assembly: ExportEffect(typeof(ColorOverlayEffectiOS), nameof(ColorOverlayEffect))]
namespace Flex.iOS.Effects
{
    public class ColorOverlayEffectiOS : PlatformEffect
    {
        protected override void OnAttached()
        {
            var effect = (ColorOverlayEffect)Element.Effects.FirstOrDefault(e => e is ColorOverlayEffect);
            if (effect == null)
                return;

            if (!(Control is UIImageView))
                return;

            SetOverlay(effect.Color);
        }

        protected override void OnDetached()
        {
            //throw new NotImplementedException();
        }

        void SetOverlay(Xamarin.Forms.Color color)
        {
            var formsImage = (Xamarin.Forms.Image)Element;
            if (formsImage?.Source == null)
                return;

            try
            {
                UIImage image = ((UIImageView)Control).Image;
                UIImage coloredImage = null;
                UIGraphics.BeginImageContextWithOptions(image.Size, false, 0.0f);
                using (CGContext context = UIGraphics.GetCurrentContext())
                {
                    context.TranslateCTM(0, image.Size.Height);
                    context.ScaleCTM(1.0f, -1.0f);

                    //var rect = new RectangleF(0, 0, (float)(image.Size.Width * image.CurrentScale), (float)(image.Size.Height * image.CurrentScale));
                    var rect = new RectangleF(0, 0, (float)(image.Size.Width), (float)(image.Size.Height));

                    // draw image, (to get transparancy mask)
                    context.SetBlendMode(CGBlendMode.Normal);
                    context.DrawImage(rect, image.CGImage);

                    // draw the color using the sourcein blend mode so its only draw on the non-transparent pixels
                    context.SetBlendMode(CGBlendMode.SourceIn);
                    context.SetFillColor(color.ToUIColor().CGColor);
                    context.FillRect(rect);

                    coloredImage = UIGraphics.GetImageFromCurrentImageContext();
                    UIGraphics.EndImageContext();
                }

                ((UIImageView)Control).Image = coloredImage;
            }
            catch (ObjectDisposedException)
            {
                return;
            }
        }
    }
}