using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Flex.Demo
{
    public partial class SecondPage : ContentPage
    {
        public SecondPage()
        {
            InitializeComponent();
        }

        private void button1_OnClicked(object sender, EventArgs e)
        {
            button1.BackgroundColor = Color.Blue;
            button2.BackgroundColor = Color.White;
        }

        private void button2_OnClicked(object sender, EventArgs e)
        {
            button1.BackgroundColor = Color.White;
            button2.BackgroundColor = Color.Blue;
        }


    }
}
