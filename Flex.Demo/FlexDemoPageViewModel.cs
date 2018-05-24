using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Flex.Demo
{
    public class FlexDemoPageViewModel : INotifyPropertyChanged
    {
        public bool IsButtonEnabled = true;

        bool isToggled;
        public bool IsToggled
        {
            get { return isToggled; }
            set { isToggled = value; OnPropertyChanged(); }
        }

        // Just demonstrates the use of Commands
        Command buttonClickedCommand;
        public Command ButtonClickedCommand => buttonClickedCommand ?? (buttonClickedCommand = new Command(() =>
        {
            Application.Current.MainPage.DisplayAlert("Hello from the View Model", "The Flex Button rocks!", "Yeah");
        }, () => IsButtonEnabled));

        // Implementation of INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}