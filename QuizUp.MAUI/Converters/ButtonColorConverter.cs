using System.Diagnostics;
using System.Globalization;

namespace QuizUp.MAUI.Converters;
public class ButtonColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? buttonIndex, CultureInfo culture)
    {
        Debug.Assert(value is not null);

        var buttonIndexString = buttonIndex as string;
        Debug.Assert(buttonIndexString is string);

        Debug.Assert(Application.Current is not null);
        Application.Current.Resources.TryGetValue("PrimaryDark", out var primaryColor);

        int index = int.Parse(buttonIndexString);
        int selectedIndex = (int)value;

        if (selectedIndex == -1)
        {
            return primaryColor;
        }

        return index == selectedIndex ? primaryColor : Color.FromRgba(100, 100, 100, 255);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}