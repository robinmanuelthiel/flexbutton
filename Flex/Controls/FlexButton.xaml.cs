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
        bool userChangedPadding;

        #region Bindable Properties

        public static readonly new BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(FlexButton), Color.Transparent);
        public new Color BackgroundColor
        {
            get { return (Color)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        public static readonly BindableProperty IconOrientationProperty = BindableProperty.Create(nameof(IconOrientation), typeof(IconOrientation), typeof(FlexButton), IconOrientation.Left);
        public IconOrientation IconOrientation
        {
            get { return (IconOrientation)GetValue(IconOrientationProperty); }
            set { SetValue(IconOrientationProperty, value); }
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

        public static readonly BindableProperty ForegroundColorProperty = BindableProperty.Create(nameof(ForegroundColor), typeof(Color), typeof(FlexButton), Color.White);
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

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(FlexButton), string.Empty);
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

        public static readonly new BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(FlexButton), new Thickness(-1));
        public new Thickness Padding
        {
            get { return (Thickness)GetValue(PaddingProperty); }
            set { SetValue(PaddingProperty, value); }
        }

        public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(ImageSource), typeof(FlexButton), null);
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        #endregion

        #region Commands

        public static readonly BindableProperty ClickedCommandProperty = BindableProperty.Create(nameof(ClickedCommand), typeof(ICommand), typeof(FlexButton), null, propertyChanged: (bindable, oldValue, newValue) => ((FlexButton)bindable).OnClickOrTouchedDownCommandPropertyChanged());
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

        #region Events

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == PaddingProperty.PropertyName)
            {
                userChangedPadding = true;
            }
            else if (propertyName == IsEnabledProperty.PropertyName)
            {
                // Set Opacity based on IsEnabled
                Opacity = IsEnabled ? 1 : 0.5;
            }
            else if (propertyName == IconProperty.PropertyName || propertyName == ForegroundColorProperty.PropertyName)
            {
                SetButtonMode();
                ColorIcon(ForegroundColor);
            }
            else if (propertyName == TextProperty.PropertyName ||
                     propertyName == IconOrientationProperty.PropertyName)
            {
                SetButtonMode();
            }

            base.OnPropertyChanged(propertyName);
        }

        void OnClickOrTouchedDownCommandPropertyChanged()
        {
            if (ClickedCommand != null)
                ClickedCommand.CanExecuteChanged += CommandCanExecuteChanged;

            if (TouchedDownCommand != null)
                TouchedDownCommand.CanExecuteChanged += CommandCanExecuteChanged;

            CommandCanExecuteChanged(this, EventArgs.Empty);
        }

        void CommandCanExecuteChanged(object sender, EventArgs e)
        {
            // Define IsEnabled state
            var canExecuteClick = ClickedCommand?.CanExecute(null);
            var canExecuteTouchedDown = TouchedDownCommand?.CanExecute(null);

            if (canExecuteClick != null && canExecuteTouchedDown != null)
                IsEnabled = canExecuteClick == true && canExecuteTouchedDown == true;
            else
                IsEnabled = canExecuteClick == true || canExecuteTouchedDown == true;
        }

        protected override void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            // Unsubscribe from command events when Command changes
            if (propertyName == ClickedCommandProperty.PropertyName && ClickedCommand != null)
                ClickedCommand.CanExecuteChanged -= CommandCanExecuteChanged;
            if (propertyName == TouchedDownCommandProperty.PropertyName && TouchedDownCommandProperty != null)
                TouchedDownCommand.CanExecuteChanged -= CommandCanExecuteChanged;

            base.OnPropertyChanging(propertyName);
        }

        #endregion

        void SetButtonMode()
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

                    // Configure Container
                    ContainerContent.HorizontalOptions = LayoutOptions.Fill;
                    Grid.SetColumnSpan(ButtonIcon, 2);
                    Grid.SetColumn(ButtonText, 1);

                    // Set Visibilities
                    ButtonText.IsVisible = false;

                    // Adjust Default Padding
                    if (!userChangedPadding)
                    {
                        Padding = new Thickness(HeightRequest * .3, HeightRequest * .3);
                        userChangedPadding = false; // Set this back to false, as the above line triggers OnPropertyChanged 
                    }

                    break;

                case ButtonMode.IconWithText:

                    // Configure Container
                    ContainerContent.HorizontalOptions = LayoutOptions.Center;
                    switch (IconOrientation)
                    {
                        case IconOrientation.Left:
                            FirstColumn.Width = new GridLength(1, GridUnitType.Star);
                            SecondColumn.Width = GridLength.Auto;
                            Grid.SetColumn(ButtonIcon, 0);
                            Grid.SetColumnSpan(ButtonIcon, 1);
                            Grid.SetColumn(ButtonText, 1);
                            break;

                        case IconOrientation.Right:
                            FirstColumn.Width = GridLength.Auto;
                            SecondColumn.Width = new GridLength(1, GridUnitType.Star);
                            Grid.SetColumn(ButtonIcon, 1);
                            Grid.SetColumnSpan(ButtonIcon, 1);
                            Grid.SetColumn(ButtonText, 0);
                            break;
                    }

                    // Set Visibilities
                    ButtonText.IsVisible = true;

                    // Adjust Default Padding
                    if (!userChangedPadding)
                    {
                        Padding = new Thickness(HeightRequest * .1, HeightRequest * .3);
                        userChangedPadding = false; // Set this back to false, as the above line triggers OnPropertyChanged 
                    }

                    break;

                case ButtonMode.TextOnly:

                    // Configure Container
                    ContainerContent.HorizontalOptions = LayoutOptions.Center;
                    Grid.SetColumnSpan(ButtonIcon, 1);
                    Grid.SetColumn(ButtonText, 0);

                    // Set Visibilities
                    ButtonText.IsVisible = true;

                    // Adjust Default Padding
                    if (!userChangedPadding)
                    {
                        Padding = new Thickness(0);
                        userChangedPadding = false; // Set this back to false, as the above line triggers OnPropertyChanged 
                    }

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
        }

        void TouchDown()
        {
            if (IsEnabled)
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
            if (IsEnabled)
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
