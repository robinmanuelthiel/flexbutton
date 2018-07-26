---
name: Bug report
about: Create a report to help us improve

---

**Describe the bug**
FlexButton cannot use icons on iOS if they are embeddedresources. It works correct if I put icon to platform-specific folder(Resources/drawable) or don't use icon at all;

**To Reproduce**
Steps to reproduce the behavior:
1. Add some flex button in xaml code;
2. Add some .NET Standard project for various resources(In my case it is called Resources);
3. Add some icon in Resources project(i put them in "Images" folder);
4. Add helper to resources project, that will return ImageSource like this:
"return ImageSource.FromResource(string.Format("My.Solution.Namespace.Resources.Images.{0}",imageName));"
5. Add MarkupExtension, that will return Source for icon like this:
"public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
                return null;
            return ImageHelper.GetImage(Source);
        }";
6. Set Icon property of FlexButton to extension:
"Icon="{extns:ImageResource sample.png}"";
7.Get NullReferenceException while page loading;

**Expected behavior**
Button should work with embedded images from other projects just like it does with ones in usual places(Platform specific folders)

**Screenshots**
If applicable, add screenshots to help explain your problem.

**Please complete the following information:**
- Which version of the FlexButton do you use?(0.8.0)
- Which version of Xamarin.Forms do you use?(3.1.0.637273)
- Which OS are you talking about?(iOS 11.4.1)
