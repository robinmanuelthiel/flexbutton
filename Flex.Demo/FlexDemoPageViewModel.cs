using Xamarin.Forms;

namespace Flex.Demo
{
    public class FlexDemoPageViewModel
    {
        // Just demonstrates the use of Commands
        public Command ButtonClickedCommand
        {
            get
            {
                return new Command(() =>
                {
                    //var test = "Do something";
                });
            }
        }
    }
}
