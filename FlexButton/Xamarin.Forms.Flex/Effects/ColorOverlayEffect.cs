﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.Forms.Flex.Effects
{
    public class ColorOverlayEffect : RoutingEffect
    {
        public Color Color { get; set; }

        public ColorOverlayEffect() : base("Xamarin.Forms.Flex.Effects.ColorOverlayEffect")
        {

        }
    }
}
