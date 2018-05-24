using System;
using Xamarin.Forms;

namespace Flex.Controls
{
    /// <summary>
    /// Override class to not apply the GestureFrameRenderer to every Frame 
    /// class in the project, but just to the one that FlexButton uses
    /// </summary>
    public class GestureFrame : Frame
    {
    }
}
