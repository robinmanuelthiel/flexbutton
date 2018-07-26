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
        private UIImageRenderingMode originalRenderingMode = UIImageRenderingMode.Automatic;
        private UIColor originalTintColor = UIColor.Black;

        protected override void OnAttached()
        {
            var effect = (ColorOverlayEffect)Element.Effects.FirstOrDefault(e => e is ColorOverlayEffect);
            if (effect == null)
                return;

            SetOverlay(effect.Color);
        }

        protected override void OnDetached()
        {
            if (Control is UIImageView imageView && imageView.Image != null)
            {
                imageView.TintColor = originalTintColor;
                imageView.Image = imageView.Image.ImageWithRenderingMode(originalRenderingMode);
            }
        }

        void SetOverlay(Color color)
        {
            var formsImage = (Xamarin.Forms.Image)Element;
            if (formsImage?.Source == null || formsImage?.IsLoading == true)
                return;
                
            if (Control is UIImageView imageView && imageView.Image != null)
            {
                originalRenderingMode = imageView.Image.RenderingMode;
                originalTintColor = imageView.TintColor;
                imageView.Image = imageView.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
                imageView.TintColor = color.ToUIColor();
            }
        }
    }
}
