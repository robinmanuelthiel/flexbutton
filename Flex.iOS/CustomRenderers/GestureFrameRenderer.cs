using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Flex.Extensions;
using Flex.iOS.CustomRenderers;
using System.ComponentModel;
using Flex.Controls;

[assembly: ExportRenderer(typeof(GestureFrame), typeof(GestureFrameRenderer))]
namespace Flex.iOS.CustomRenderers
{
    public class GestureFrameRenderer : FrameRenderer
    {
        UILongPressGestureRecognizer pressGestureRecognizer;

        public static new void Init()
        {
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            // Fix Xamarin.Forms Frame BackgroundColor Bug (https://github.com/xamarin/Xamarin.Forms/issues/2218)
            if (e.PropertyName == nameof(Element.BackgroundColor))
                this.Layer.BackgroundColor = Element.BackgroundColor.ToUIColor().CGColor;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                // Fix Xamarin.Forms Frame BackgroundColor Bug (https://github.com/xamarin/Xamarin.Forms/issues/2218)
                this.Layer.BackgroundColor = e.NewElement.BackgroundColor.ToUIColor().CGColor;

                if (!e.NewElement.GestureRecognizers.Any())
                    return;

                if (!e.NewElement.GestureRecognizers.Any(x => x.GetType() == typeof(TouchGestureRecognizer)))
                    return;

                pressGestureRecognizer = new UILongPressGestureRecognizer(() =>
                {
                    if (pressGestureRecognizer.State == UIGestureRecognizerState.Began)
                    {
                        foreach (var recognizer in Element.GestureRecognizers.Where(x => x.GetType() == typeof(TouchGestureRecognizer)))
                        {
                            if (recognizer is TouchGestureRecognizer touchGestureRecognizer)
                            {
                                touchGestureRecognizer?.TouchDown();
                            }
                        }
                    }
                    else if (pressGestureRecognizer.State == UIGestureRecognizerState.Ended)
                    {
                        foreach (var recognizer in Element.GestureRecognizers.Where(x => x.GetType() == typeof(TouchGestureRecognizer)))
                        {
                            if (recognizer is TouchGestureRecognizer touchGestureRecognizer)
                            {
                                touchGestureRecognizer?.TouchUp();
                            }
                        }
                    }
                });

                pressGestureRecognizer.MinimumPressDuration = 0.0;

                AddGestureRecognizer(pressGestureRecognizer);
            }
        }
    }
}