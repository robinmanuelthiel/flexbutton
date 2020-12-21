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

                // FIX: This fixes another Xamarin.Forms bug introduced in Xamarin.Forms 4.7, that messes with corner radius
                // in iOS. To find out, of the bug has been resolved, just take out the following lines and check, if the border
                // radiusses render correctly.
                // Bug: https://github.com/xamarin/Xamarin.Forms/issues/2405 and https://github.com/xamarin/Xamarin.Forms/issues/7823
                if (e.NewElement != null)
                {
                    NativeView.Layer.CornerRadius = e.NewElement.CornerRadius;
                    NativeView.ClipsToBounds = false;
                }
                // END FIX

                if (!e.NewElement.GestureRecognizers.Any())
                    return;

                if (!e.NewElement.GestureRecognizers.Any(x => x.GetType() == typeof(TouchGestureRecognizer)))
                    return;                

                var hasLeftButtonBounds = false;
               
                pressGestureRecognizer = new UILongPressGestureRecognizer(() =>
                {
                    var touchedPoint = pressGestureRecognizer.LocationInView(this);
                    var isInsideButtonBounds = e.NewElement.Bounds.Contains(touchedPoint.ToPoint());
                    if (!isInsideButtonBounds)
                    {
                        // Pointer left the bounds of the button.
                        hasLeftButtonBounds = true;
                        FireTouchCanceled();
                    }

                    if (pressGestureRecognizer.State == UIGestureRecognizerState.Began)
                    {
                        // Reset
                        hasLeftButtonBounds = false;
                        FireTouchDown();
                    }                    
                    else if (pressGestureRecognizer.State == UIGestureRecognizerState.Ended)
                    {
                        // Only fire, when pointer has never left the button bounds
                        if (!hasLeftButtonBounds)
                        {
                            FireTouchUp();
                        }
                    }
                });

                pressGestureRecognizer.MinimumPressDuration = 0.0;
                AddGestureRecognizer(pressGestureRecognizer);
            }
        }

        private void FireTouchDown()
        {
            foreach (var recognizer in Element.GestureRecognizers.Where(x => x.GetType() == typeof(TouchGestureRecognizer)))
            {
                if (recognizer is TouchGestureRecognizer touchGestureRecognizer)
                {
                    touchGestureRecognizer?.TouchDown();
                }
            }
        }

        private void FireTouchUp()
        {
            foreach (var recognizer in Element.GestureRecognizers.Where(x => x.GetType() == typeof(TouchGestureRecognizer)))
            {
                if (recognizer is TouchGestureRecognizer touchGestureRecognizer)
                {
                    touchGestureRecognizer?.TouchUp();
                }
            }
        }

        private void FireTouchCanceled()
        {
            foreach (var recognizer in Element.GestureRecognizers.Where(x => x.GetType() == typeof(TouchGestureRecognizer)))
            {
                if (recognizer is TouchGestureRecognizer touchGestureRecognizer)
                {
                    touchGestureRecognizer?.TouchCanceled();
                }
            }
        }
    }
}