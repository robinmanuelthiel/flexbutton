using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Flex.Effects;
using Flex.Extensions;
using Xamarin.Forms;

namespace Flex.Controls
{
    public partial class FlexButton : ContentView
    {
        ButtonMode mode;
        private bool isEnabled = true;

        #region Bindable Properties

        public static readonly new BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(FlexButton), Color.Transparent);
        public new Color BackgroundColor
        {
            get { return (Color)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        public static readonly new BindableProperty OrientationProperty = BindableProperty.Create(nameof(Orientation), typeof(Model.Orientation), typeof(FlexButton), Model.Orientation.Left, propertyChanged: OnOrientationChanged);
        public new Model.Orientation Orientation
        {
            get { return (Model.Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
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

        #endregion

        #region Commands

        public static readonly BindableProperty ClickedCommandProperty = BindableProperty.Create(nameof(ClickedCommand), typeof(ICommand), typeof(FlexButton), null, propertyChanged: (bo, o, n) => ((FlexButton)bo).OnClickCommandPropertyChanged());

        public ICommand ClickedCommand
        {
            get { return (ICommand)GetValue(ClickedCommandProperty); }
            set { SetValue(ClickedCommandProperty, value); }
        }

        public static BindableProperty TouchedDownCommandProperty = BindableProperty.Create(nameof(TouchedDownCommand), typeof(ICommand), typeof(FlexButton), null);
        public ICommand TouchedDownCommand
        {
            get { return (ICommand)GetValue(TouchedDownCommandProperty); }
            set { SetValue(TouchedDownCommandProperty, value); }
        }

        public static BindableProperty TouchedUpCommandProperty = BindableProperty.Create(nameof(TouchedUpCommand), typeof(ICommand), typeof(FlexButton), null);
        public ICommand TouchedUpCommand
        {
            get { return (ICommand)GetValue(TouchedUpCommandProperty); }
            set { SetValue(TouchedUpCommandProperty, value); }
        }

        #endregion

        static void IconOrForegroundColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var flexButton = ((FlexButton)bindable);
            flexButton.SetButtonMode();
            flexButton.ColorIcon(flexButton.ForegroundColor);
        }

        private static void OnOrientationChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var flexButton = ((FlexButton)bindable);
            flexButton.SetButtonMode();
        }

        private void OnClickCommandPropertyChanged()
        {
            if (ClickedCommand != null)
            {
                ClickedCommand.CanExecuteChanged += CommandCanExecuteChanged;
                CommandCanExecuteChanged(this, EventArgs.Empty);
            }
            else
            {
                isEnabled = true;
            }
        }

        private void CommandCanExecuteChanged(object sender, EventArgs e)
        {
            ICommand cmd = ClickedCommand;
            if (cmd != null)
            {
                isEnabled = cmd.CanExecute(null);
            }
        }

        static void TextOrOrientationChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var flexButton = ((FlexButton)bindable);
            flexButton.SetButtonMode();
        }

		protected override void OnPropertyChanging([CallerMemberName] string propertyName = null)
		{
            if (propertyName == ClickedCommandProperty.PropertyName)
            {
                ICommand cmd = ClickedCommand;
                if (cmd != null)
                    cmd.CanExecuteChanged -= CommandCanExecuteChanged;
            }

            base.OnPropertyChanging(propertyName);
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

                    switch (Orientation)
                    {
                        case Model.Orientation.Left:
                            FirstColumn.Width = new GridLength(1, GridUnitType.Star);
                            SecondColumn.Width = GridLength.Auto;

                            Grid.SetColumn(ButtonIcon, 0);
                            Grid.SetColumnSpan(ButtonIcon, 1);
                            Grid.SetColumn(ButtonText, 1);
                            break;
                        case Model.Orientation.Rigth:
                            FirstColumn.Width = GridLength.Auto;
                            SecondColumn.Width = new GridLength(1, GridUnitType.Star);

                            Grid.SetColumn(ButtonIcon, 1);
                            Grid.SetColumnSpan(ButtonIcon, 1);
                            Grid.SetColumn(ButtonText, 0);
                            break;
                    }

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

            TouchRecognizer.TouchDown += TouchDown;
            TouchRecognizer.TouchUp += TouchUp;
            SizeChanged += FlexButton_SizeChanged;

            ButtonIcon.SizeChanged += ButtonIcon_SizeChanged;
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

        void TouchDown()
        {
            if (isEnabled)
            {
                TouchedDown?.Invoke(this, null);
                TouchedDownCommand?.Execute(null);

                Container.BackgroundColor = HighlightBackgroundColor;
                ButtonText.TextColor = HighlightForegroundColor;
                ColorIcon(HighlightForegroundColor);
            }
        }

        void TouchUp()
        {
            if (isEnabled)
            {
                TouchedUp?.Invoke(this, null);
                TouchedUpCommand?.Execute(null);
                Clicked?.Invoke(this, null);
                ClickedCommand?.Execute(null);

                Container.BackgroundColor = BackgroundColor;
                ButtonText.TextColor = ForegroundColor;
                ColorIcon(ForegroundColor);
            }
        }

        void ColorIcon(Color color)
        {
            ButtonIcon.Effects.Clear();
            ButtonIcon.Effects.Add(new ColorOverlayEffect() { Color = color });
        }
    }
}
