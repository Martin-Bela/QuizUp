using System.Globalization;

namespace QuizUp.MAUI.Converters;
public class ButtonColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        Application.Current.Resources.TryGetValue("PrimaryDark", out var primaryColor);

        int buttonIndex = int.Parse(parameter as string);
        int selectedIndex = (int)value;

        if (selectedIndex == -1)
        {
            return primaryColor;
        }

        return buttonIndex == selectedIndex ? primaryColor : Color.FromRgba(100, 100, 100, 255);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}