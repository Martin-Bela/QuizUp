using System.Diagnostics;
using System.Globalization;

namespace QuizUp.MAUI.Converters;
public class AnswerButtonColorConverter : IValueConverter
{
    public object Convert(object? selectedAnswerIndex, Type targetType, object? buttonIndex, CultureInfo culture)
    {
        Debug.Assert(selectedAnswerIndex != null && buttonIndex != null && Application.Current != null);

        var selectedAnswerIndexInt = TryParseIndex(selectedAnswerIndex);
        var buttonIndexInt = TryParseIndex(buttonIndex);

        var colorName = buttonIndexInt switch
        {
            0 => "FirstAnswerColor",
            1 => "SecondAnswerColor",
            2 => "ThirdAnswerColor",
            3 => "FourthAnswerColor",
            _ => "DefaultAnswerColor",
        };

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

    private static int TryParseIndex(object index)
    {
        var parsingSucceeded = int.TryParse(index.ToString(), out var indexInt);
        return parsingSucceeded ? indexInt : -1;
    }
}
