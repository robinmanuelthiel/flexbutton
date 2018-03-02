using Xamarin.Forms;

namespace Flex.Effects
{
    public class ColorOverlayEffect : RoutingEffect
    {
        public Color Color { get; set; }

        public ColorOverlayEffect() : base("Flex.Effects.ColorOverlayEffect")
        {
        }
    }
}
