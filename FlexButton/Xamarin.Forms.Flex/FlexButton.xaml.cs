using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Flex.Effects;
using Xamarin.Forms;

namespace Xamarin.Forms.Flex
{ 
    //[ContentProperty(nameof(ButtonContent))]
    public partial class FlexButton : ContentView
    {
        #region Color Properties

        public static readonly new BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(FlexButton), Color.Transparent);
        public new Color BackgroundColor
        {
            get { return (Color)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        // TODO: Border Color does not wokr on Android at the moment due to a Xamarin.Forms bug
        //public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(FlexButton), Color.Red);
        //public Color BorderColor
        //{
        //    get { return (Color)GetValue(BorderColorProperty); }
        //    set { SetValue(BorderColorProperty, value); }
        //}

        public static readonly BindableProperty HighlightColorProperty = BindableProperty.Create(nameof(HighlightColor), typeof(Color), typeof(FlexButton), Color.Transparent);
        public Color HighlightColor
        {
            get { return (Color)GetValue(HighlightColorProperty); }
            set { SetValue(HighlightColorProperty, value); }
        }

        public static readonly BindableProperty IconColorProperty = BindableProperty.Create(nameof(IconColor), typeof(Color), typeof(FlexButton), Color.White);
        public Color IconColor
        {
            get { return (Color)GetValue(IconColorProperty); }
            set { SetValue(IconColorProperty, value);  Setup(); }
        }

        public static readonly BindableProperty HighlightIconColorProperty = BindableProperty.Create(nameof(HighlightIconColor), typeof(Color), typeof(FlexButton), Color.White);
        public Color HighlightIconColor
        {
            get { return (Color)GetValue(HighlightIconColorProperty); }
            set { SetValue(HighlightIconColorProperty, value); }
        }

        #endregion

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(FlexButton), 0);
        public int CornerRadius
        {
            get { return (int)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(ImageSource), typeof(FlexButton), null);
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set
            {
                SetValue(IconProperty, value);
                Setup();
            }
        }

        public View ButtonContent
        {
            get { return ContainerContent.Content; }
            set { ContainerContent.Content = value; }
        }

        public Action TouchedDown = null;
        public Action TouchedUp = null;

        public FlexButton()
        {
            InitializeComponent();
            BindingContext = this;

            TouchRecognizer.TouchDown += TouchDown;
            TouchRecognizer.TouchUp += TouchUp;
        }

        private void Setup()
        {
            ColorIcon(IconColor);
        }

        private void TouchDown()
        {
            TouchedDown?.Invoke();

            Container.BackgroundColor = HighlightColor;

            ColorIcon(HighlightIconColor);

            //ButtonIcon.Effects.Remove(IconColorOverlayEffect);
            //ButtonIcon.Effects.Add(HighlightIconColorOverlayEffect);

        }

        private void TouchUp()
        {
            TouchedUp?.Invoke();

            Container.BackgroundColor = BackgroundColor;

            ColorIcon(IconColor);

            //ButtonIcon.Effects.Remove(HighlightIconColorOverlayEffect);
            //ButtonIcon.Effects.Add(IconColorOverlayEffect);
        }

        private void ColorIcon(Color color)
        {
            ButtonIcon.Effects.Clear();
            ButtonIcon.Effects.Add(new ColorOverlayEffect() { Color = color });
        }
    }
}
