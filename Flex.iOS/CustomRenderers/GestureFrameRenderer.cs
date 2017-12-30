using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Flex.Extensions;
using Flex.iOS.CustomRenderers;

[assembly: ExportRenderer(typeof(Frame), typeof(GestureFrameRenderer))]
namespace Flex.iOS.CustomRenderers
{
    public class GestureFrameRenderer : FrameRenderer
    {
        UILongPressGestureRecognizer pressGestureRecognizer;

        public static new void Init()
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


                //Control.UserInteractionEnabled = true;

                pressGestureRecognizer = new UILongPressGestureRecognizer(() =>
                {
                    if (pressGestureRecognizer.State == UIGestureRecognizerState.Began)
                    {
                        foreach (var recognizer in Element.GestureRecognizers.Where(x => x.GetType() == typeof(TouchGestureRecognizer)))
                        {
                            var touchGestureRecognizer = recognizer as TouchGestureRecognizer;
                            if (touchGestureRecognizer != null)
                            {
                                if (touchGestureRecognizer.TouchDownCommand != null)
                                    touchGestureRecognizer.TouchDownCommand.Execute(touchGestureRecognizer.TouchDownCommandParameter);

                                if (touchGestureRecognizer.TouchDown != null)
                                    touchGestureRecognizer.TouchDown();
                            }
                        }
                    }
                    else if (pressGestureRecognizer.State == UIGestureRecognizerState.Ended)
                    {
                        foreach (var recognizer in Element.GestureRecognizers.Where(x => x.GetType() == typeof(TouchGestureRecognizer)))
                        {
                            var touchGestureRecognizer = recognizer as TouchGestureRecognizer;
                            if (touchGestureRecognizer != null)
                            {
                                if (touchGestureRecognizer.TouchUpCommand != null)
                                    touchGestureRecognizer.TouchUpCommand.Execute(touchGestureRecognizer.TouchUpCommandParameter);

                                if (touchGestureRecognizer.TouchUp != null)
                                    touchGestureRecognizer.TouchUp();
                            }
                        }
                    }
                });

                pressGestureRecognizer.MinimumPressDuration = 0.0;
                //pressGestureRecognizer.Delegate = gestureDelegate;

                AddGestureRecognizer(pressGestureRecognizer);
            }
        }
    }
}