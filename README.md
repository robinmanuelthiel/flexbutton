# FlexButton
Flexible button control for Xamarin.Forms with events for different states, color overlays and adjustable shapes and paddings. Supports iOS and Android.

[![NuGet](https://img.shields.io/nuget/v/Forms.Controls.FlexButton.svg?label=NuGet&style=flat-square)](https://www.nuget.org/packages/Forms.Controls.FlexButton/)

**Features:**
- Adjustable button size
- Icons with adjustable size
- Color overlays for background and icon (normal and pressed state)
- Events for button press and release
- Round button (corner radius)

## How to use
**Add the [NuGet package](https://www.nuget.org/packages/Forms.Controls.FlexButton/) to the Xamarin.Forms project and all platform projects**
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
Take a look a the [Demo Project](/Demo) in this repository for a full sample.

![Preview](/Design/FlexButton.gif)

## API Reference
| Property | Default | Description |
|------------------|---------|-------------|
| `BackgroundColor` | `Transparent` | Background color of the button |
| `HighlightBackgroundColor` | `Transparent` | Background color of the button when pressed down |
| `IconColor` | `White` | Foreground color overlay for icon |
| `CornerRadiusProperty` | `0` | Button borner radius |
| `HighlightIconColor` | `White` | Foreground color overlay for icon when pressed down |
| `IconPadding` | 30% of button width and height | Inside distance from icon to button borders |

| Event | Description |
|------------------|---------|
| `TouchedDown` | Triggered, when button got pressed down |
| `TouchedUp` | Triggered, when button got released |

