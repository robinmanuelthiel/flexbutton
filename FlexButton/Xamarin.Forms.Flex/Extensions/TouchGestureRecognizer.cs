using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Xamarin.Forms.Flex.Extensions
{
    public class TouchGestureRecognizer : Element, IGestureRecognizer
    {
        public static readonly BindableProperty TouchUpCommandProperty = BindableProperty.Create(nameof(TouchUpCommand), typeof(ICommand), typeof(TouchGestureRecognizer), (object)null, BindingMode.OneWay, (BindableProperty.ValidateValueDelegate)null, (BindableProperty.BindingPropertyChangedDelegate)null, (BindableProperty.BindingPropertyChangingDelegate)null, (BindableProperty.CoerceValueDelegate)null, (BindableProperty.CreateDefaultValueDelegate)null);
        public static readonly BindableProperty TouchUpCommandPropertyParameter = BindableProperty.Create(nameof(TouchUpCommandParameter), typeof(object), typeof(TouchGestureRecognizer), (object)null, BindingMode.TwoWay, (BindableProperty.ValidateValueDelegate)null, (BindableProperty.BindingPropertyChangedDelegate)null, (BindableProperty.BindingPropertyChangingDelegate)null, (BindableProperty.CoerceValueDelegate)null, (BindableProperty.CreateDefaultValueDelegate)null);

        public ICommand TouchUpCommand
        {
            get { return (ICommand)this.GetValue(TouchUpCommandProperty); }
            set { this.SetValue(TouchUpCommandProperty, (object)value); }
        }

        public object TouchUpCommandParameter
        {
            get { return this.GetValue(TouchUpCommandPropertyParameter); }
            set { this.SetValue(TouchUpCommandPropertyParameter, value); }
        }

        public static readonly BindableProperty TouchDownCommandProperty = BindableProperty.Create(nameof(TouchDownCommand), typeof(ICommand), typeof(TouchGestureRecognizer), (object)null, BindingMode.OneWay, (BindableProperty.ValidateValueDelegate)null, (BindableProperty.BindingPropertyChangedDelegate)null, (BindableProperty.BindingPropertyChangingDelegate)null, (BindableProperty.CoerceValueDelegate)null, (BindableProperty.CreateDefaultValueDelegate)null);
        public static readonly BindableProperty TouchDownCommandParameterProperty = BindableProperty.Create(nameof(TouchDownCommandParameter), typeof(object), typeof(TouchGestureRecognizer), (object)null, BindingMode.TwoWay, (BindableProperty.ValidateValueDelegate)null, (BindableProperty.BindingPropertyChangedDelegate)null, (BindableProperty.BindingPropertyChangingDelegate)null, (BindableProperty.CoerceValueDelegate)null, (BindableProperty.CreateDefaultValueDelegate)null);

        public ICommand TouchDownCommand
        {
            get { return (ICommand)this.GetValue(TouchDownCommandProperty); }
            set { this.SetValue(TouchDownCommandProperty, (object)value); }
        }

        public object TouchDownCommandParameter
        {
            get { return this.GetValue(TouchDownCommandParameterProperty); }
            set { this.SetValue(TouchDownCommandParameterProperty, value); }
        }

        public Action TouchDown;
        public Action TouchUp;
    }
}
