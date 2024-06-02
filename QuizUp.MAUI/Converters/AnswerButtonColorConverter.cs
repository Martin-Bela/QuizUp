using System.Diagnostics;
using System.Globalization;

namespace QuizUp.MAUI.Converters;
public class AnswerButtonColorConverter : IValueConverter
{
    public object Convert(object? selectedAnswerIndex, Type targetType, object? buttonIndex, CultureInfo culture)
    {
        Debug.Assert(Application.Current != null);

        var selectedAnswerIndexInt = ConverterUtils.TryParseIndex(selectedAnswerIndex);
        var buttonIndexInt = ConverterUtils.TryParseIndex(buttonIndex);

        var colorName = ConverterUtils.AnswerIndexToColorName(buttonIndexInt);

        Application.Current.Resources.TryGetValue(colorName, out var colorObject);

        var color = (Color)colorObject;

        if (selectedAnswerIndexInt == buttonIndexInt)
        {
            // Make the color a bit darker
            color = color.WithLuminosity(color.GetLuminosity() * 0.5f);
        }

        return color;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
