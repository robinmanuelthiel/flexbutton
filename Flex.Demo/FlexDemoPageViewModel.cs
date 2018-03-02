using System;
using System.Windows.Input;
using Xamarin.Forms;
namespace Flex.Demo
{
    public class FlexDemoPageViewModel
    {
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

        public FlexDemoPageViewModel()
        {
        }
    }
}
