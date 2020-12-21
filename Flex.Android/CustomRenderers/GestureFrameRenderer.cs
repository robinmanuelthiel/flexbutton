using System.Linq;
using Android.Content;
using Android.Views;
using Flex.Android.CustomRenderers;
using Flex.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Flex.Controls;

[assembly: ExportRenderer(typeof(GestureFrame), typeof(GestureFrameRenderer))]
namespace Flex.Android.CustomRenderers
{
    public class GestureFrameRenderer : Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer
    {
        public GestureFrameRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                if (!e.NewElement.GestureRecognizers.Any())
                    return;

                if (!e.NewElement.GestureRecognizers.Any(x => x.GetType() == typeof(TouchGestureRecognizer)))
                    return;

                var hasLeftButtonBounds = false;

                Control.Touch += (object sender, TouchEventArgs te) =>
                {
                    var isInsideButtonBounds = (te.Event.GetX() > 0 && te.Event.GetX() <= Width) && (te.Event.GetY() > 0 && te.Event.GetY() <= Height);
                    if (!isInsideButtonBounds)
                    {
                        // Pointer left the bounds of the button.
                        hasLeftButtonBounds = true;
                        FireTouchCanceled();
                    }

                    switch (te.Event.Action)
                    {
                        case MotionEventActions.Down:
                            // Reset
                            hasLeftButtonBounds = false;
                            FireTouchDown();
                            break;

                        case MotionEventActions.Up:
                            // Only fire, when pointer has never left the button bounds
                            if (!hasLeftButtonBounds)
                            {
                                FireTouchUp();
                            }
                            break;                                                    

                        case MotionEventActions.Cancel:
                            foreach (var recognizer in Element.GestureRecognizers.Where(x => x.GetType() == typeof(TouchGestureRecognizer)))
                            {
                                FireTouchCanceled();
                            }
                            break;
                    }
                };
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