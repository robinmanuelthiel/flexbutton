using System.Windows.Input;
using Xamarin.Forms;

namespace Flex.Demo
{
    public class FlexDemoPageViewModel
    {
        public bool IsButtonEnabled = true;

        // Just demonstrates the use of Commands
        Command buttonClickedCommand;
        public Command ButtonClickedCommand => buttonClickedCommand ?? (buttonClickedCommand = new Command(() =>
        {
            Application.Current.MainPage.DisplayAlert("Hello from the View Model", "The Flex Button rocks!", "Yeah");
        }, () => IsButtonEnabled));
    }
}
