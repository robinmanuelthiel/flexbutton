using Xamarin.Forms;
using System;
using Flex.Extensions;
using Flex.Controls;

namespace Flex.Demo
{
    public partial class FlexDemoPage : ContentPage
    {
        public FlexDemoPage()
        {
            InitializeComponent();
            BindingContext = new FlexDemoPageViewModel();
        }

        void DemoButton_TouchedDown(object sender, EventArgs e)
        {
            ButtonStatus.Text = "Pressed";
        }

        void DemoButton_TouchedUp(object sender, EventArgs e)
        {
            ButtonStatus.Text = "Released";
        }

        void ToggleIsEnabled_Clicked(object sender, EventArgs e)
        {
            ((FlexDemoPageViewModel)BindingContext).IsButtonEnabled = !((FlexDemoPageViewModel)BindingContext).IsButtonEnabled;
            ((FlexDemoPageViewModel)BindingContext).ButtonClickedCommand.ChangeCanExecute();
        }

        void ToggleIconOrientation_Clicked(object sender, EventArgs e)
        {
            WideButton.IconOrientation = WideButton.IconOrientation == IconOrientation.Left ? IconOrientation.Right : IconOrientation.Left;
        }

        void ButtonWithoutBackground_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Hello from Code Behind", "The Flex Button rocks! " + ((FlexDemoPageViewModel)BindingContext).IsToggled.ToString(), "Yeah");
        }

        public void Handle_Toggled(object sender, ToggledEventArgs e)
        {
            ((FlexButton)sender).Text = e.Value.ToString();
        }

        public void ToggleIsToggled_Clicked(object sender, EventArgs e)
        {
            ((FlexDemoPageViewModel)BindingContext).IsToggled = !((FlexDemoPageViewModel)BindingContext).IsToggled;
        }
    }
}
