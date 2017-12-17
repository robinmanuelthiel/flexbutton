using System;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms.Flex.iOS.Effects;
using Xamarin.Forms.Flex.Effects;
using System.Linq;
using UIKit;

[assembly: ResolutionGroupName("Xamarin.Forms.Flex.Effects")]
[assembly: ExportEffect(typeof(ColorOverlayEffectiOS), nameof(ColorOverlayEffect))]
namespace Xamarin.Forms.Flex.iOS.Effects
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

        private void SetOverlay(Xamarin.Forms.Color color)
        {
            var formsImage = (Xamarin.Forms.Image)Element;
            if (formsImage?.Source == null)
                return;

            try
            {

            }
            catch (ObjectDisposedException)
            {
                return;
            }
        }
    }
}