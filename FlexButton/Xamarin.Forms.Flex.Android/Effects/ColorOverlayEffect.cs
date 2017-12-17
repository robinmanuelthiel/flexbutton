using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Flex.Android.Effects;
using Xamarin.Forms.Flex.Effects;

[assembly: ResolutionGroupName("Xamarin.Forms.Flex.Effects")]
[assembly: ExportEffect(typeof(ColorOverlayEffectAndroid), nameof(ColorOverlayEffect))]
namespace Xamarin.Forms.Flex.Android.Effects
{
    public class ColorOverlayEffectAndroid : PlatformEffect
    {
        //private Drawable originalImage;

        protected override void OnAttached()
        {
            var effect = (ColorOverlayEffect)Element.Effects.FirstOrDefault(e => e is ColorOverlayEffect);
            if (effect == null)
                return;

            if (!(Control is ImageView))
                return;

            //originalImage = ((ImageView)Control).Drawable;

            SetOverlay(effect.Color);
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (!(Control is ImageView))
                return;

            Debug.WriteLine(args.PropertyName);
            //if (!args.PropertyName.Equals("Source") || !args.PropertyName.Equals("Renderer"))
            //    return;

            var effect = (ColorOverlayEffect)Element.Effects.FirstOrDefault(e => e is ColorOverlayEffect);
            if (effect == null)
                return;

            SetOverlay(effect.Color);
        }

        private void SetOverlay(Xamarin.Forms.Color color)
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