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

                Control.Touch += (object sender, TouchEventArgs te) =>
                {
                    switch (te.Event.Action)
                    {
                        case MotionEventActions.Down:
                            foreach (var recognizer in Element.GestureRecognizers.Where(x => x.GetType() == typeof(TouchGestureRecognizer)))
                            {
                                if (recognizer is TouchGestureRecognizer touchGestureRecognizer)
                                {
                                    touchGestureRecognizer?.TouchDown();
                                }
                            }
                            break;

                        case MotionEventActions.Up:
                        case MotionEventActions.Cancel:
                            foreach (var recognizer in Element.GestureRecognizers.Where(x => x.GetType() == typeof(TouchGestureRecognizer)))
                            {
                                if (recognizer is TouchGestureRecognizer touchGestureRecognizer)
                                {
                                    touchGestureRecognizer?.TouchUp();
                                }
                            }
                            break;
                    }
                };
            }
        }
    }
}