using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Android.Graphics;
using Android.Widget;
using Flex.Android.Effects;
using Flex.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("Flex.Effects")]
[assembly: ExportEffect(typeof(ColorOverlayEffectAndroid), nameof(ColorOverlayEffect))]
namespace Flex.Android.Effects
{
    public class ColorOverlayEffectAndroid : PlatformEffect
    {
        protected override void OnAttached()
        {
            var effect = (ColorOverlayEffect)Element.Effects.FirstOrDefault(e => e is ColorOverlayEffect);
            if (effect == null)
                return;

            if (!(Control is ImageView))
                return;

            SetOverlay(effect.Color);
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (!(Control is ImageView))
                return;

            var effect = (ColorOverlayEffect)Element.Effects.FirstOrDefault(e => e is ColorOverlayEffect);
            if (effect == null)
                return;

            SetOverlay(effect.Color);
        }

        void SetOverlay(Xamarin.Forms.Color color)
        {
            var formsImage = (Xamarin.Forms.Image)Element;
            if (formsImage?.Source == null)
                return;

            try
            {
                var drawable = ((ImageView)Control).Drawable.Mutate();
                drawable.SetColorFilter(color.ToAndroid(), PorterDuff.Mode.SrcAtop);
                drawable.Alpha = color.ToAndroid().A;

                ((ImageView)Control).SetImageDrawable(drawable);
                ((IVisualElementController)Element).NativeSizeChanged();
            }
            catch (ObjectDisposedException)
            {
                return;
            }
        }

        protected override void OnDetached()
        {
            //if (!(Control is ImageView) || ((ImageView)Control).Drawable == null || originalImage == null)
            //    return;

            //((ImageView)Control).SetImageDrawable(originalImage);
        }
    }
}