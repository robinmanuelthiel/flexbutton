# FlexButton

Flexible button control for Xamarin.Forms with events for different states, color overlays and adjustable shapes and paddings. Supports iOS and Android.

[![NuGet](https://img.shields.io/nuget/v/Forms.Controls.FlexButton.svg?label=NuGet&style=flat-square)](https://www.nuget.org/packages/Forms.Controls.FlexButton/)

**Features:**

- Adjustable button size and shape
- Icons with adjustable size
- Color overlays for background and icon (normal and pressed state)
- Events for button hold and release
- Round button and Pill button (using corner radius)
- Icon only, text only or mixed mode
- Customizable Borders
- Toggle Mode

## How to use

**Add the [NuGet package](https://www.nuget.org/packages/Forms.Controls.FlexButton/) to the Xamarin.Forms project and all platform projects**

```bash
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
    ForegroundColor="#ffffff"
    HighlightForegroundColor="#49516F"
    BackgroundColor="#6279B8"
    HighlightBackgroundColor="#8EA4D2" />
```

## Preview

Take a look a the [Demo Project](/Flex.Demo) in this repository for a full sample.

![Preview](/Design/FlexButton.gif)

## API Reference

| Property | Default | Description |
|------------------|---------|-------------|
| `Icon` | `null` | Name of the icon file to use |
| `Text` | `string.Empty` | Button text to be displayed |
| `FontSize` | Default | Font size of the button text |
| `ForegroundColor` | `White` | Foreground color overlay for icon and text |
| `BackgroundColor` | `Transparent` | Background color of the button |
| `BorderColor` | `Transparent` | Border color of the button |
| `HighlightForegroundColor` | `White` | Foreground color overlay for icon and text when pressed down |
| `HighlightBackgroundColor` | `Transparent` | Background color of the button when pressed down |
| `HighlightBorderColor` | `Transparent` | Background color of the border when pressed down |
| `CornerRadius` | `0` | Button borner radius |
| `Padding` | 30% of height, 10-30% of width  | Inside distance from icon to button borders |
| `IconPadding` | 0 | Additional Padding around the icon to control distance to text |
| `IconOrientation` | `IconOrientation.Left` | Positions the icon on a button that has icon and text |
| `BorderThickness` | `0` | Width of the border in each direction |
| `ToggleMode` | `false` | Sets the button in Toggle Mode |
| `IsToggled` | `false` | Represents the Toggle state, when Toggle Mode is enabled |
| `IconTintEnabled` | `true` | Enables tinting of the icon (Set to false, if your icon is colorized) |

| Event | Description |
|------------------|---------|
| `TouchedDown` | Triggered, when button got pressed down |
| `TouchedUp` | Triggered, when button got released |
| `Clicked` | Same as `TouchedUp` |
| `Toggled` | Triggered, when the button got toggled on or off |
