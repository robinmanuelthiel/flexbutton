using System.Linq;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Froms.Flex.Android.CustomRenderers;
using Xamarin.Forms.Flex.Extensions;

[assembly: ExportRenderer(typeof(Frame), typeof(GestureFrameRenderer))]
namespace Xamarin.Froms.Flex.Android.CustomRenderers
{
    public class GestureFrameRenderer : Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                if (!e.NewElement.GestureRecognizers.Any())
                    return;

                if (!e.NewElement.GestureRecognizers.Any(x => x.GetType() == typeof(TouchGestureRecognizer)))
                    return;

                Control.Touch += (object sender, TouchEventArgs te) =>
                {
                    switch (te.Event.Action)
                    {
                        case MotionEventActions.Down:
                            foreach (var recognizer in Element.GestureRecognizers.Where(x => x.GetType() == typeof(TouchGestureRecognizer)))
                            {
                                if (recognizer is TouchGestureRecognizer touchGestureRecognizer)
                                {
                                    if (touchGestureRecognizer.TouchDownCommand != null)
                                        touchGestureRecognizer.TouchDownCommand.Execute(touchGestureRecognizer.TouchDownCommandParameter);

                                    if (touchGestureRecognizer.TouchDown != null)
                                        touchGestureRecognizer.TouchDown();
                                }
                            }
                            break;
                        case MotionEventActions.Up:
                            foreach (var recognizer in Element.GestureRecognizers.Where(x => x.GetType() == typeof(TouchGestureRecognizer)))
                            {
                                if (recognizer is TouchGestureRecognizer touchGestureRecognizer)
                                {
                                    if (touchGestureRecognizer.TouchUpCommand != null)
                                        touchGestureRecognizer.TouchUpCommand.Execute(touchGestureRecognizer.TouchUpCommandParameter);

                                    if (touchGestureRecognizer.TouchUp != null)
                                        touchGestureRecognizer.TouchUp();
                                }
                            }
                            break;
                    }
                };
            }
        }
    }
}