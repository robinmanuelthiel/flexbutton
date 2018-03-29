using System;
using System.Linq;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Flex.Android.CustomRenderers;
using Flex.Controls;
using Flex.Extensions; 
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Frame), typeof(GestureFrameRenderer))]
namespace Flex.Android.CustomRenderers
{
    public class GestureFrameRenderer : Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer
    {
        public GestureFrameRenderer(Context context) : base(context)
        {
        }
        
        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas); 
            DrawBorder(canvas);
        }

        /// <summary>
        /// Draws a border in case <see cref="FlexButton.BorderSize"/> is greater 0.
        /// </summary>
        private void DrawBorder(Canvas canvas)
        {
            if (Element.Parent is FlexButton flexButton)
            {
                if(flexButton.BorderSize <= 0)
                    return; //Nothing to draw here!

                //Get the correct scale between the canvas and element size
                var scale = canvas.Width / Element.Width;

                //Little helper that calculates the corresponding canvas size to the given forms size
                int ScaleToCanvasSize (double formsValue) => (int)Math.Ceiling(formsValue * scale);

                //Calculate the scaled radius and border size
                var cornerRadius = ScaleToCanvasSize(Element.CornerRadius);
                var strokeWidth = ScaleToCanvasSize(flexButton.BorderSize);

                //Create the rect and paint
                var rect = new RectF(0, 0, canvas.Width, canvas.Height);
                var paint = new Paint(PaintFlags.AntiAlias)
                {
                    Color = flexButton.BorderColor.ToAndroid(),
                    StrokeWidth = strokeWidth
                };
                paint.SetStyle(Paint.Style.Stroke);

                //Draw! 
                canvas.DrawRoundRect(rect, cornerRadius, cornerRadius, paint);
            }
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