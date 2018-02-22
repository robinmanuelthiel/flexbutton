using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flex.Effects;
using Xamarin.Forms;
using Flex.Extensions;

namespace Flex.Controls
{
    public partial class FlexButton : ContentView
    {
        private ButtonMode mode;

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

        public static readonly BindableProperty HighlightBackgroundColorProperty = BindableProperty.Create(nameof(HighlightBackgroundColor), typeof(Color), typeof(FlexButton), Color.Transparent);
        public Color HighlightBackgroundColor
        {
            get { return (Color)GetValue(HighlightBackgroundColorProperty); }
            set { SetValue(HighlightBackgroundColorProperty, value); }
        }

        public static readonly BindableProperty ForegroundColorProperty = BindableProperty.Create(nameof(ForegroundColor), typeof(Color), typeof(FlexButton), Color.White, propertyChanged: IconOrForegroundColorPropertyChanged);
        public Color ForegroundColor
        {
            get { return (Color)GetValue(ForegroundColorProperty); }
            set { SetValue(ForegroundColorProperty, value); }
        }

        public static readonly BindableProperty HighlightForegroundColorProperty = BindableProperty.Create(nameof(HighlightForegroundColor), typeof(Color), typeof(FlexButton), Color.White);
        public Color HighlightForegroundColor
        {
            get { return (Color)GetValue(HighlightForegroundColorProperty); }
            set { SetValue(HighlightForegroundColorProperty, value); }
        }

        #endregion

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(FlexButton), Device.GetNamedSize(NamedSize.Default, typeof(Label)));
        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(FlexButton), string.Empty, propertyChanged: TextOrOrientationChanged);
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(FlexButton), 0);
        public int CornerRadius
        {
            get { return (int)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly new BindableProperty PaddingProperty =
            BindableProperty.Create(
                nameof(Padding),
                typeof(Thickness),
                typeof(FlexButton),
                new Thickness(-1));

        public new Thickness Padding
        {
            get { return (Thickness)GetValue(PaddingProperty); }
            set { SetValue(PaddingProperty, value); }
        }

        public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(ImageSource), typeof(FlexButton), null, propertyChanged: IconOrForegroundColorPropertyChanged);
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        static void IconOrForegroundColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var flexButton = ((FlexButton)bindable);
            flexButton.SetButtonMode();
            flexButton.ColorIcon(flexButton.ForegroundColor);
        }

        static void TextOrOrientationChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var flexButton = ((FlexButton)bindable);
            flexButton.SetButtonMode();
        }

        private void SetButtonMode()
        {
            if (Icon != null && Text.Length > 0)
            {
                mode = ButtonMode.IconWithText;
            }
            else if (Icon != null && Text.Length == 0)
            {
                mode = ButtonMode.IconOnly;
            }
            else if (Icon == null && Text.Length > 0)
            {
                mode = ButtonMode.TextOnly;
            }

            switch (mode)
            {
                case ButtonMode.IconOnly:
                    ContainerContent.HorizontalOptions = LayoutOptions.Fill;
                    Grid.SetColumnSpan(ButtonIcon, 2);
                    Grid.SetColumn(ButtonText, 1);
                    ButtonText.IsVisible = false;
                    break;
                case ButtonMode.IconWithText:
                    ContainerContent.HorizontalOptions = LayoutOptions.Center;
                    Grid.SetColumnSpan(ButtonIcon, 1);
                    Grid.SetColumn(ButtonText, 1);
                    ButtonText.IsVisible = true;
                    break;
                case ButtonMode.TextOnly:
                    ContainerContent.HorizontalOptions = LayoutOptions.Center;
                    Grid.SetColumnSpan(ButtonIcon, 1);
                    Grid.SetColumn(ButtonText, 0);
                    ButtonText.IsVisible = true;
                    break;
            }
        }

        public event EventHandler<EventArgs> TouchedDown;
        public event EventHandler<EventArgs> TouchedUp;
        public event EventHandler<EventArgs> Clicked;

        public FlexButton()
        {
            InitializeComponent();
            BindingContext = this;

            TouchRecognizer.TouchDown += TouchDown;
            TouchRecognizer.TouchUp += TouchUp;
            SizeChanged += FlexButton_SizeChanged;

            this.ButtonIcon.SizeChanged += ButtonIcon_SizeChanged;
        }

        void ButtonIcon_SizeChanged(object sender, EventArgs e)
        {
            ColorIcon((ForegroundColor));
        }

        void FlexButton_SizeChanged(object sender, EventArgs e)
        {
            // Set Padding to 30% of with / height by default if no specific padding is set
            if (Padding.Equals(new Thickness(-1)))
            {
                switch (mode)
                {
                    default:
                    case ButtonMode.IconOnly:
                        Padding = new Thickness(WidthRequest * .3, HeightRequest * .3);
                        break;
                    case ButtonMode.IconWithText:
                    case ButtonMode.TextOnly:
                        Padding = new Thickness(WidthRequest * .1, HeightRequest * .3);
                        break;
                }
            }
        }

        private void TouchDown()
        {
            TouchedDown?.Invoke(this, null);

            Container.BackgroundColor = HighlightBackgroundColor;
            ButtonText.TextColor = HighlightForegroundColor;
            ColorIcon(HighlightForegroundColor);
        }

        private void TouchUp()
        {
            TouchedUp?.Invoke(this, null);
            Clicked?.Invoke(this, null);

            Container.BackgroundColor = BackgroundColor;
            ButtonText.TextColor = ForegroundColor;
            ColorIcon(ForegroundColor);
        }

        private void ColorIcon(Color color)
        {
            ButtonIcon.Effects.Clear();
            ButtonIcon.Effects.Add(new ColorOverlayEffect() { Color = color });
        }
    }
}
