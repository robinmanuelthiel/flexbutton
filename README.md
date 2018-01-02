# FlexButton
Flexible button control for Xamarin.Forms that notifies you when it got **PRESSED and RELEASED** and let's you adjust **HIGLIGHT COLORS**, **ICONS** and **PADDINGS**.

[![NuGet](https://img.shields.io/nuget/v/Forms.Controls.FlexButton.svg?label=NuGet&style=flat-square)](https://www.nuget.org/packages/Forms.Controls.FlexButton/)

## How to use
**Add the [NuGet package](https://www.nuget.org/packages/Forms.Controls.FlexButton/) to the Xamarin.Forms project**
```
PM> Install-Package Forms.Controls.FlexButton
```

**[iOS only] Initialize the control**

In your **AppDelegate.cs** call this after `Forms.Init();`
```csharp
FlexButton.Init();
```

**Add the XML namespace**
```xml
xmlns:flex="clr-namespace:Flex.Controls;assembly=Flex"
```

**Add the control**
```xml
<flex:FlexButton
    WidthRequest="76"
    HeightRequest="76"
    CornerRadius="38"
    HorizontalOptions="Center"
    Icon="lightbulb.png"
    IconColor="#ffffff"
    HighlightIconColor="#49516F"
    BackgroundColor="#6279B8"
    HighlightBackgroundColor="#8EA4D2"
    TouchedDown="DemoButton_TouchedDown"
    TouchedUp="DemoButton_TouchedUp"/>
```
## Preview
![Preview](/Design/FlexButton.gif)

## API Reference
| Property | Default | Description |
|------------------|---------|-------------|
| BackgroundColor | Transparent | Background color of the button |
| HighlightBackgroundColor | Transparent | Background color of the button when pressed down |
| IconColor | White | Foreground color overlay for icon |
| CornerRadiusProperty | 0 | Button borner radius |
| HighlightIconColor | White | Foreground color overlay for icon when pressed down |
| IconPadding | 30% of button width and height | Inside distance from icon to button borders |

| Event | Description |
|------------------|---------|
| TouchedDown | Triggered, when button got pressed down |
| TouchedUp | Triggered, when button got released |

