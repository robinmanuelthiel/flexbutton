using System;
using System.ComponentModel;
using Xamarin.Forms;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using CoreGraphics;
using Flex.iOS.CustomRenderers;
using Flex.Extensions;

[assembly: ExportRenderer(typeof(IconView), typeof(IconViewRenderer))]
namespace Flex.iOS.CustomRenderers
{
    public class IconViewRenderer : ViewRenderer<IconView, UIImageView>
    {
        private bool isDisposed;

        protected override void Dispose(bool disposing)
        {
            if (isDisposed)
            {
                return;
            }

            if (disposing && base.Control != null)
            {
                UIImage image = base.Control.Image;
                UIImage uIImage = image;
                if (image != null)
                {
                    uIImage.Dispose();
                    uIImage = null;
                }
            }

            isDisposed = true;
            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<IconView> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                UIImageView uIImageView = new UIImageView(CGRect.Empty)
                {
                    ContentMode = UIViewContentMode.ScaleAspectFit,
                    ClipsToBounds = true
                };
                SetNativeControl(uIImageView);
            }
            if (e.NewElement != null)
            {
                SetImage(e.OldElement);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == IconView.SourceProperty.PropertyName)
            {
                SetImage(null);
            }
            else if (e.PropertyName == IconView.ForegroundProperty.PropertyName)
            {
                SetImage(null);
            }
        }

        private void SetImage(IconView previous = null)
        {
            if (previous == null && Element.Source != null)
            {
                var uiImage = new UIImage(Element.Source);
                uiImage = uiImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
                Control.TintColor = Element.Foreground.ToUIColor();
                Control.Image = uiImage;
                if (!isDisposed)
                {
                    ((IVisualElementController)Element).NativeSizeChanged();
                }
            }
        }
    }
}