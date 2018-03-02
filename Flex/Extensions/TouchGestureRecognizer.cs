using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Flex.Extensions
{
    public class TouchGestureRecognizer : Element, IGestureRecognizer
    {
        public Action TouchDown;
        public Action TouchUp;
    }
}
